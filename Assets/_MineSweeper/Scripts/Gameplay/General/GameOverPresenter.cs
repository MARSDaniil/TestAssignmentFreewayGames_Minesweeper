using System;
using Zenject;

public class GameOverPresenter : IInitializable, IDisposable {
    #region Fields

    private readonly GameplayUIController m_hud;
    private readonly IBoardService m_boardService;
    private readonly ISceneLoader m_sceneLoader;
    private readonly GameFlowController m_gameFlowController;

    #endregion

    #region Public

    public GameOverPresenter(
        GameplayUIController a_hud,
        IBoardService a_boardService,
        ISceneLoader a_sceneLoader,
        GameFlowController a_gameFlowController) {
        m_hud = a_hud;
        m_boardService = a_boardService;
        m_sceneLoader = a_sceneLoader;
        m_gameFlowController = a_gameFlowController;
    }

    public void Initialize() {
        if (m_hud != null) {
            m_hud.gameOverPanel.Hide(true);

            if (m_hud.gameOverPanel != null) {
                m_hud.gameOverPanel.e_onRestartPressedEvent += OnRestartPressed;
                m_hud.gameOverPanel.e_onBackToMenuPressedEvent += OnBackToMenuPressed;
            }
        }

        m_boardService.e_onGameLostEvent += OnWinGame;
        m_boardService.e_onGameWinEvent += OnLoseGame;
    }

    public void Dispose() {
        if (m_hud != null && m_hud.gameOverPanel != null) {
            m_hud.gameOverPanel.e_onRestartPressedEvent -= OnRestartPressed;
            m_hud.gameOverPanel.e_onBackToMenuPressedEvent -= OnBackToMenuPressed;
        }

        m_boardService.e_onGameLostEvent -= OnWinGame;
        m_boardService.e_onGameWinEvent -= OnLoseGame;
    }

    #endregion

    #region Private

    private void OnWinGame() {
        OpenEndOfGame();
        if (m_hud != null) {
            m_hud.gameOverPanel.SetResult(Constants.Locale.SID_YOU_WIN);
        }

    }

    private void OnLoseGame() {
        OpenEndOfGame();

        if (m_hud != null) {
            m_hud.gameOverPanel.SetResult(Constants.Locale.SID_YOU_LOSE);
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
    }

    private void OnBackToMenuPressed() {
        m_sceneLoader.LoadSingleAsync(Constants.Scene.Lobby);
    }

    #endregion
}
