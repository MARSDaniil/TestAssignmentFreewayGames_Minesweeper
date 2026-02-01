using System;
using System.Collections.Generic;
using UnityEngine;

public class BoardService : IBoardService {
    #region Fields

    public event Action<Vector2Int> e_onCellChangedEvent;
    public event Action e_onGameLostEvent;
    public event Action e_onGameWinEvent;

    public int SizeX {
        get {
            return m_sizeX;
        }
    }
    public int SizeY {
        get {
            return m_sizeY;
        }
    }

    private readonly BoardConfig m_config;

    private int m_sizeX;
    private int m_sizeY;
    private int m_minesCount;

    private BoardCell[,] m_cells;

    private bool m_isMinesPlaced;
    private bool m_isGameEnded;

    private int m_openedSafeCellsCount;
    private int m_totalSafeCellsCount;

    #endregion

    #region Public

    public BoardService(BoardConfig a_config) {
        m_config = a_config;
    }

    public void CreateNewBoard() {
        m_sizeX = Mathf.Max(1, m_config.sizeX);
        m_sizeY = Mathf.Max(1, m_config.sizeY);

        int maxMines = Mathf.Max(0, (m_sizeX * m_sizeY) - 1);
        m_minesCount = Mathf.Clamp(m_config.minesCount, 0, maxMines);

        m_cells = new BoardCell[m_sizeX, m_sizeY];

        m_isMinesPlaced = false;
        m_isGameEnded = false;

        m_openedSafeCellsCount = 0;
        m_totalSafeCellsCount = (m_sizeX * m_sizeY) - m_minesCount;

        for (int x = 0; x < m_sizeX; x++) {
            for (int y = 0; y < m_sizeY; y++) {
                m_cells[x, y] = new BoardCell {
                    IsMine = false,
                    IsOpened = false,
                    IsFlagged = false,
                    AdjacentMines = 0
                };
            }
        }
    }

    public BoardCell GetCell(Vector2Int a_position) {
        if (!IsInBounds(a_position)) {
            return default;
        }

        return m_cells[a_position.x, a_position.y];
    }

    public void ToggleFlag(Vector2Int a_position) {
        if (m_isGameEnded) {
            return;
        }

        if (!IsInBounds(a_position)) {
            return;
        }

        BoardCell cell = m_cells[a_position.x, a_position.y];

        if (cell.IsOpened) {
            return;
        }

        cell.IsFlagged = !cell.IsFlagged;
        m_cells[a_position.x, a_position.y] = cell;

        RaiseCellChanged(a_position);
    }

    public void OpenCell(Vector2Int a_position) {
        if (m_isGameEnded) {
            return;
        }

        if (!IsInBounds(a_position)) {
            return;
        }

        BoardCell cell = m_cells[a_position.x, a_position.y];

        if (cell.IsOpened || cell.IsFlagged) {
            return;
        }

        if (!m_isMinesPlaced) {
            PlaceMines(a_position);
            ComputeAdjacentNumbers();
            m_isMinesPlaced = true;
        }

        cell = m_cells[a_position.x, a_position.y];

        if (cell.IsMine) {
            RevealAllMines();
            m_isGameEnded = true;
            e_onGameLostEvent?.Invoke();
            return;
        }

        FloodOpenFrom(a_position);

        if (m_openedSafeCellsCount >= m_totalSafeCellsCount) {
            m_isGameEnded = true;
            e_onGameWinEvent?.Invoke();
        }
    }

    #endregion

    #region Private

    private void FloodOpenFrom(Vector2Int a_start) {
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        queue.Enqueue(a_start);

        while (queue.Count > 0) {
            Vector2Int pos = queue.Dequeue();

            if (!IsInBounds(pos)) {
                continue;
            }

            BoardCell cell = m_cells[pos.x, pos.y];

            if (cell.IsOpened || cell.IsFlagged) {
                continue;
            }

            if (cell.IsMine) {
                continue;
            }

            cell.IsOpened = true;
            m_cells[pos.x, pos.y] = cell;

            m_openedSafeCellsCount++;
            RaiseCellChanged(pos);

            if (cell.AdjacentMines != 0) {
                continue;
            }

            foreach (Vector2Int n in GetNeighbors8(pos)) {
                if (!IsInBounds(n)) {
                    continue;
                }

                BoardCell nextCell = m_cells[n.x, n.y];

                if (nextCell.IsOpened || nextCell.IsFlagged) {
                    continue;
                }

                if (nextCell.IsMine) {
                    continue;
                }

                queue.Enqueue(n);
            }
        }
    }

    private void PlaceMines(Vector2Int a_firstClick) {
        HashSet<Vector2Int> forbidden = BuildForbiddenPositions(a_firstClick);

        List<Vector2Int> candidates = new List<Vector2Int>(m_sizeX * m_sizeY);

        for (int x = 0; x < m_sizeX; x++) {
            for (int y = 0; y < m_sizeY; y++) {
                Vector2Int pos = new Vector2Int(x, y);

                if (forbidden.Contains(pos)) {
                    continue;
                }

                candidates.Add(pos);
            }
        }

        for (int i = candidates.Count - 1; i > 0; i--) {
            int j = UnityEngine.Random.Range(0, i + 1);
            Vector2Int tmp = candidates[i];
            candidates[i] = candidates[j];
            candidates[j] = tmp;
        }

        int minesToPlace = Mathf.Min(m_minesCount, candidates.Count);

        for (int i = 0; i < minesToPlace; i++) {
            Vector2Int pos = candidates[i];

            BoardCell cell = m_cells[pos.x, pos.y];
            cell.IsMine = true;
            m_cells[pos.x, pos.y] = cell;
        }

        m_totalSafeCellsCount = (m_sizeX * m_sizeY) - minesToPlace;
    }

    private HashSet<Vector2Int> BuildForbiddenPositions(Vector2Int a_center) {
        HashSet<Vector2Int> forbidden = new HashSet<Vector2Int>();

        int r = 0;

        for (int dx = -r; dx <= r; dx++) {
            for (int dy = -r; dy <= r; dy++) {
                Vector2Int pos = new Vector2Int(a_center.x + dx, a_center.y + dy);

                if (IsInBounds(pos)) {
                    forbidden.Add(pos);
                }
            }
        }

        if (!forbidden.Contains(a_center)) {
            forbidden.Add(a_center);
        }

        return forbidden;
    }

    private void ComputeAdjacentNumbers() {
        for (int x = 0; x < m_sizeX; x++) {
            for (int y = 0; y < m_sizeY; y++) {
                Vector2Int pos = new Vector2Int(x, y);
                BoardCell cell = m_cells[x, y];

                if (cell.IsMine) {
                    cell.AdjacentMines = 0;
                    m_cells[x, y] = cell;
                    continue;
                }

                byte mines = 0;

                foreach (Vector2Int n in GetNeighbors8(pos)) {
                    if (!IsInBounds(n)) {
                        continue;
                    }

                    if (m_cells[n.x, n.y].IsMine) {
                        mines++;
                    }
                }

                cell.AdjacentMines = mines;
                m_cells[x, y] = cell;
            }
        }
    }

    private void RevealAllMines() {
        for (int x = 0; x < m_sizeX; x++) {
            for (int y = 0; y < m_sizeY; y++) {
                BoardCell cell = m_cells[x, y];

                if (!cell.IsMine) {
                    continue;
                }

                if (cell.IsOpened) {
                    continue;
                }

                cell.IsOpened = true;
                m_cells[x, y] = cell;

                RaiseCellChanged(new Vector2Int(x, y));
            }
        }
    }

    private bool IsInBounds(Vector2Int a_pos) {
        if (a_pos.x < 0 || a_pos.y < 0) {
            return false;
        }

        if (a_pos.x >= m_sizeX || a_pos.y >= m_sizeY) {
            return false;
        }

        return true;
    }

    private IEnumerable<Vector2Int> GetNeighbors8(Vector2Int a_pos) {
        for (int dx = -1; dx <= 1; dx++) {
            for (int dy = -1; dy <= 1; dy++) {
                if (dx == 0 && dy == 0) {
                    continue;
                }

                yield return new Vector2Int(a_pos.x + dx, a_pos.y + dy);
            }
        }
    }

    private void RaiseCellChanged(Vector2Int a_position) {
        e_onCellChangedEvent?.Invoke(a_position);
    }

    #endregion
}
