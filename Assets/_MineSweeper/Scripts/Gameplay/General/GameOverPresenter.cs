using System;
using Zenject;

public class GameOverPresenter : IInitializable, IDisposable {
    #region Fields

    private readonly GameplayUIController m_hud;
    private readonly IBoardService m_boardService;
    private readonly ISceneLoader m_sceneLoader;
    private readonly HudPresenter m_hudPresenter;

    #endregion

    #region Public

    public GameOverPresenter(
        GameplayUIController a_hud,
        IBoardService a_boardService,
        ISceneLoader a_sceneLoader,
        GameFlowController a_gameFlowController,
        HudPresenter a_hudPresenter) {
        m_hud = a_hud;
        m_boardService = a_boardService;
        m_sceneLoader = a_sceneLoader;
        m_hudPresenter = a_hudPresenter;
    }

    public void Initialize() {
        if (m_hud != null) {
            m_hud.gameOverPanel.Hide(true);

            if (m_hud.gameOverPanel != null) {
                m_hud.gameOverPanel.e_onRestartPressedEvent += OnRestartPressed;
                m_hud.gameOverPanel.e_onBackToMenuPressedEvent += OnBackToMenuPressed;
            }
        }

        m_boardService.e_onGameLostEvent += OnLoseGame;
        m_boardService.e_onGameWinEvent += OnWinGame;
    }

    public void Dispose() {
        if (m_hud != null && m_hud.gameOverPanel != null) {
            m_hud.gameOverPanel.e_onRestartPressedEvent -= OnRestartPressed;
            m_hud.gameOverPanel.e_onBackToMenuPressedEvent -= OnBackToMenuPressed;
        }

        m_boardService.e_onGameLostEvent -= OnLoseGame;
        m_boardService.e_onGameWinEvent -= OnWinGame;
    }

    #endregion

    #region Private

    private void OnWinGame() {
        OpenEndOfGame();
        if (m_hud != null) {
            m_hud.gameOverPanel.SetResult(Locale.GetText(Constants.Locale.SID_YOU_WIN));
        }

    }

    private void OnLoseGame() {
        OpenEndOfGame();

        if (m_hud != null) {
            m_hud.gameOverPanel.SetResult(Locale.GetText(Constants.Locale.SID_YOU_LOSE));
        }


    }

    private void OpenEndOfGame() {
        if (m_hud != null) {
            m_hud.gameOverPanel.Show();
        }
    }

    private void OnRestartPressed() {
        if (m_hud != null) {
            m_hud.gameOverPanel.Hide(false);
        }
        m_hudPresenter.ResetGame();
    }

    private void OnBackToMenuPressed() {
        m_sceneLoader.LoadSingleAsync(Constants.Scene.Lobby);
    }

    #endregion
}
