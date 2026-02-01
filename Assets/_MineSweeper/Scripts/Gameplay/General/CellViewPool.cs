using System.Collections.Generic;
using UnityEngine;

public class CellViewPool {
    #region Fields

    private readonly Stack<CellView> m_pool = new Stack<CellView>();
    private readonly Dictionary<Vector2Int, CellView> m_activeCells = new Dictionary<Vector2Int, CellView>();

    private readonly CellView m_prefab;
    private readonly Transform m_parent;

    #endregion

    #region Public

    public CellViewPool(CellView a_prefab, Transform a_parent) {
        m_prefab = a_prefab;
        m_parent = a_parent;
    }

    public CellView Get(Vector2Int a_position) {
        CellView cell = m_pool.Count > 0
            ? m_pool.Pop()
            : Object.Instantiate(m_prefab, m_parent);

        cell.gameObject.SetActive(true);
        cell.SetPosition(a_position.x, a_position.y);
        cell.ResetVisual();

        m_activeCells[a_position] = cell;
        return cell;
    }

    public CellView GetByPosition(Vector2Int a_position) {
        return m_activeCells[a_position];
    }

    public void ReleaseAll() {
        foreach (CellView cell in m_activeCells.Values) {
            cell.gameObject.SetActive(false);
            m_pool.Push(cell);
        }

        m_activeCells.Clear();
    }

    #endregion
}
