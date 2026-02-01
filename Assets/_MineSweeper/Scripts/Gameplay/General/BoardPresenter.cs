using System;
using UnityEngine;
using Zenject;

public class BoardPresenter : IInitializable, IDisposable {
    #region Fields

    private readonly IBoardService m_boardService;
    private readonly CellViewPool m_cellPool;
    private readonly BoardView m_boardView;

    #endregion

    #region Public

    public BoardPresenter(
        IBoardService a_boardService,
        CellViewPool a_cellPool,
        BoardView a_boardView) {
        m_boardService = a_boardService;
        m_cellPool = a_cellPool;
        m_boardView = a_boardView;
    }

    public void Initialize() {
        m_boardService.e_onCellChangedEvent += OnCellChanged;
    }

    public void Dispose() {
        m_boardService.e_onCellChangedEvent -= OnCellChanged;
    }

    public void BuildBoard() {
        m_cellPool.ReleaseAll();

        for (int x = 0; x < m_boardView.SizeX; x++) {
            for (int y = 0; y < m_boardView.SizeY; y++) {
                Vector2Int pos = new Vector2Int(x, y);
                CellView cell = m_cellPool.Get(pos);

                cell.ResetVisual();
            }
        }
    }

    #endregion

    #region Private

    private void OnCellChanged(Vector2Int a_position) {
        BoardCell cell = m_boardService.GetCell(a_position);
        CellView view = m_cellPool.GetByPosition(a_position);
        view.ApplyCellData(cell);
    }

    #endregion
}
