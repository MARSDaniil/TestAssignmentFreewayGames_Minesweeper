using System;
using UnityEngine;
using Zenject;

public class GameFlowController : IInitializable, IDisposable {
    #region Fields

    private readonly IBoardService m_boardService;
    private readonly BoardPresenter m_boardPresenter;
    private readonly GameplayUIController m_hudController;
    private readonly GameInputController m_inputController;
    private readonly BoardCameraController m_boardCameraController;

    private GameState m_gameState;

    #endregion

    #region Public

    public GameFlowController(
        IBoardService a_boardService,
        BoardPresenter a_boardPresenter,
        GameplayUIController a_hudController,
        GameInputController a_inputController,
        BoardCameraController a_boardCameraController) {
        m_boardService = a_boardService;
        m_boardPresenter = a_boardPresenter;
        m_hudController = a_hudController;
        m_inputController = a_inputController;
        m_boardCameraController = a_boardCameraController;
    }

    public void Initialize() {
        m_inputController.e_onRestartPressedEvent += OnRestartPressed;
        m_boardService.e_onGameLostEvent += OnGameLost;
        m_boardService.e_onGameWinEvent += OnGameWin;

        StartNewGame();
    }

    public void Dispose() {
        m_inputController.e_onRestartPressedEvent -= OnRestartPressed;
        m_boardService.e_onGameLostEvent -= OnGameLost;
        m_boardService.e_onGameWinEvent -= OnGameWin;
    }

    public void OnRestartPressed() {
        StartNewGame();
    }

    #endregion


    #region Private

    private void StartNewGame() {
        m_boardService.CreateNewBoard();
        m_boardPresenter.BuildBoard();
        m_boardCameraController.FitToBoard();
    }

 


    private void OnGameLost() {
        m_gameState = GameState.Lose;
        m_hudController.ShowLose();
        m_inputController.BlockInput();
    }

    private void OnGameWin() {
        m_gameState = GameState.Win;
        m_hudController.ShowWin();
        m_inputController.BlockInput();
    }

    #endregion
}
