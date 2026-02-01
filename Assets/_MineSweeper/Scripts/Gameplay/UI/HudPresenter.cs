using System;
using UnityEngine;
using Zenject;

public class HudPresenter : IInitializable, IDisposable, ITickable {
    #region Fields

    private readonly IBoardService m_boardService;
    private readonly GameplayUIController m_hud;
    private readonly GameFlowController m_gameFlow;

    private bool m_isRunning;
    private float m_elapsed;

    #endregion

    #region Public

    public HudPresenter(
        IBoardService a_boardService,
        GameplayUIController a_hud,
        GameFlowController a_gameFlow) {
        m_boardService = a_boardService;
        m_hud = a_hud;
        m_gameFlow = a_gameFlow;
    }

    public void Initialize() {
        if (m_hud != null && m_hud.stateOfGames != null) {
            m_hud.stateOfGames.e_onPressedEvent += OnFacePressed;
        }

        m_boardService.e_onFlagsChangedEvent += OnFlagsChanged;
        m_boardService.e_onGameLostEvent += OnLose;
        m_boardService.e_onGameWinEvent += OnWin;
        m_boardService.e_onFirstCellOpenedEvent += OnFirstCellOpened;

        ResetHud();
    }

    public void Dispose() {
        if (m_hud != null && m_hud.stateOfGames != null) {
            m_hud.stateOfGames.e_onPressedEvent -= OnFacePressed;
        }

        m_boardService.e_onFlagsChangedEvent -= OnFlagsChanged;
        m_boardService.e_onGameLostEvent -= OnLose;
        m_boardService.e_onGameWinEvent -= OnWin;
        m_boardService.e_onFirstCellOpenedEvent -= OnFirstCellOpened;
    }

    private void OnFirstCellOpened() {
        m_isRunning = true;
    }

    public void Tick() {
        if (!m_isRunning) {
            return;
        }

        m_elapsed += Time.deltaTime;

        if (m_hud != null) {
            m_hud.SetTimerSeconds(Mathf.FloorToInt(m_elapsed));
        }
    }

    public void ResetHud() {
        m_elapsed = 0f;
        m_isRunning = false;

        if (m_hud != null) {
            m_hud.SetFaceNormal();
            m_hud.SetTimerSeconds(0);
            m_hud.SetFlagsLeft(m_boardService.MinesCount - m_boardService.FlagsPlaced);
        }
    }

    #endregion

    #region Private

    private void OnFacePressed() {
        m_gameFlow.OnRestartPressed();
        ResetHud();
    }

    private void OnFlagsChanged(int a_flagsPlaced) {
        if (m_hud == null) {
            return;
        }

        m_hud.SetFlagsLeft(m_boardService.MinesCount - a_flagsPlaced);
    }

    private void OnLose() {
        m_isRunning = false;

        if (m_hud != null) {
            m_hud.ShowLose();
        }
    }

    private void OnWin() {
        m_isRunning = false;

        if (m_hud != null) {
            m_hud.ShowWin();
        }
    }

    #endregion
}
