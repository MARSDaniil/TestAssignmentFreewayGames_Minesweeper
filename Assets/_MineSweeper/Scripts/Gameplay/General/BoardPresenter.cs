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

        float step = m_boardView.CellSize + m_boardView.Spacing;

        float boardWidth = ((m_boardService.SizeX - 1) * step) + m_boardView.CellSize;
        float boardHeight = ((m_boardService.SizeY - 1) * step) + m_boardView.CellSize;

        Vector2 offset = new Vector2(
            -boardWidth * 0.5f + m_boardView.CellSize * 0.5f,
            -boardHeight * 0.5f + m_boardView.CellSize * 0.5f
        );

        for (int x = 0; x < m_boardService.SizeX; x++) {
            for (int y = 0; y < m_boardService.SizeY; y++) {
                Vector2Int pos = new Vector2Int(x, y);
                CellView cell = m_cellPool.Get(pos);

                Vector3 worldPos = new Vector3(
                    m_boardView.Origin.x + offset.x + (x * step),
                    m_boardView.Origin.y + offset.y + (y * step),
                    0f
                );

                cell.transform.position = worldPos;
                cell.transform.localScale = Vector3.one;

                BoardCell data = m_boardService.GetCell(pos);
                cell.ApplyCellData(data);
            }
        }
    }


    #endregion

    #region Private

    private void OnCellChanged(Vector2Int a_position) {
        CellView view = m_cellPool.GetByPosition(a_position);
        if (view == null) {
            return;
        }

        BoardCell data = m_boardService.GetCell(a_position);
        view.ApplyCellData(data);
    }

    #endregion
}
