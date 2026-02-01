using Zenject;
using UnityEngine;
using System;

public class GameInputRouter : IInitializable, IDisposable {
    #region Fields

    private readonly IBoardService m_boardService;
    private readonly CellViewPool m_cellPool;

    #endregion

    #region Public

    public GameInputRouter(
        IBoardService a_boardService,
        CellViewPool a_cellPool) {
        m_boardService = a_boardService;
        m_cellPool = a_cellPool;
    }

    public void Initialize() {
        m_cellPool.e_onCellInputEvent += OnCellInput;
    }

    public void Dispose() {
        m_cellPool.e_onCellInputEvent -= OnCellInput;
    }

    #endregion

    #region Private

    private void OnCellInput(Vector2Int a_position, InputActionType a_action) {
        if (a_action == InputActionType.Open) {
            m_boardService.OpenCell(a_position);
            return;
        }

        m_boardService.ToggleFlag(a_position);
    }

    #endregion
}
