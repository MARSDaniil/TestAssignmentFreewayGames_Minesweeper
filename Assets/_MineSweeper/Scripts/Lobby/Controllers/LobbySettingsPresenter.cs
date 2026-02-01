using System;
using Zenject;

public class LobbySettingsPresenter : IInitializable, IDisposable {
    #region Fields

    private const int MinSize = 5;
    private const int MaxSize = 20;
    private const int MinMines = 5;
    private const int MaxMines = 20;

    private readonly LobbyUIController m_view;
    private readonly BoardConfig m_boardConfig;

    private WrapIntSelector m_sizeSelector;
    private WrapIntSelector m_minesSelector;

    #endregion

    #region Public

    public LobbySettingsPresenter(
        LobbyUIController a_view,
        BoardConfig a_boardConfig) {
        m_view = a_view;
        m_boardConfig = a_boardConfig;
    }

    public void Initialize() {
        LobbyStartButtonsPanel panel = m_view.lobbyStartButtonsPanel;

        int startSize = m_boardConfig.sizeX > 0 ? m_boardConfig.sizeX : 10;
        int startMines = m_boardConfig.minesCount > 0 ? m_boardConfig.minesCount : 10;

        m_sizeSelector = new WrapIntSelector(startSize, MinSize, MaxSize);
        m_minesSelector = new WrapIntSelector(startMines, MinMines, MaxMines);

        panel.sizeCount.e_onReduceEvent += OnSizeReduce;
        panel.sizeCount.e_onIncreaseEvent += OnSizeIncrease;

        panel.bombCount.e_onReduceEvent += OnMinesReduce;
        panel.bombCount.e_onIncreaseEvent += OnMinesIncrease;

        ApplyToView();
        ApplyToConfig();
    }


    public void Dispose() {
        LobbyStartButtonsPanel panel = m_view.lobbyStartButtonsPanel;

        panel.sizeCount.e_onReduceEvent -= OnSizeReduce;
        panel.sizeCount.e_onIncreaseEvent -= OnSizeIncrease;

        panel.bombCount.e_onReduceEvent -= OnMinesReduce;
        panel.bombCount.e_onIncreaseEvent -= OnMinesIncrease;
    }

    #endregion

    #region Private

    private void OnSizeReduce() {
        m_sizeSelector.Decrease();
        ApplyToView();
        ApplyToConfig();
    }

    private void OnSizeIncrease() {
        m_sizeSelector.Increase();
        ApplyToView();
        ApplyToConfig();
    }

    private void OnMinesReduce() {
        m_minesSelector.Decrease();
        ApplyToView();
        ApplyToConfig();
    }

    private void OnMinesIncrease() {
        m_minesSelector.Increase();
        ApplyToView();
        ApplyToConfig();
    }

    private void ApplyToView() {
        LobbyStartButtonsPanel panel = m_view.lobbyStartButtonsPanel;

        panel.sizeCount.SetCounter(m_sizeSelector.Value.ToString());
        panel.bombCount.SetCounter(m_minesSelector.Value.ToString());
    }

    private void ApplyToConfig() {
        int size = m_sizeSelector.Value;

        m_boardConfig.sizeX = size;
        m_boardConfig.sizeY = size;
        m_boardConfig.minesCount = m_minesSelector.Value;
    }

    #endregion
}
