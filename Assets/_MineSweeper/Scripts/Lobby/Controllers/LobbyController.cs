using System;
using Zenject;
public class LobbyController : IInitializable, IDisposable {
    #region Fields

    private readonly LobbyUIController m_view;
    private readonly ISceneLoader m_sceneLoader;
    #endregion

    #region Public

    public LobbyController(
        LobbyUIController a_view,
        ISceneLoader a_sceneLoader
        ) {
        m_view = a_view;
        m_sceneLoader = a_sceneLoader;
    }

    public void Initialize() {
        //TODO
        bool hasSave = false;
        m_view.lobbyStartButtonsPanel.SetContinueAvailable(hasSave);

        m_view.lobbyStartButtonsPanel.e_onPlayPressedEvent += OnPlayPressed;
        m_view.lobbyStartButtonsPanel.e_onContinuePressedEvent += OnContinuePressed;
    }

    public void Dispose() {
        m_view.lobbyStartButtonsPanel.e_onPlayPressedEvent -= OnPlayPressed;
        m_view.lobbyStartButtonsPanel.e_onContinuePressedEvent -= OnContinuePressed;
    }

    #endregion

    #region Private

    private void OnPlayPressed() {
        m_sceneLoader.LoadSingleAsync(Constants.Scene.Gameplay);
    }

    private void OnContinuePressed() {
        //TODO
        return;
    }

    #endregion
}

