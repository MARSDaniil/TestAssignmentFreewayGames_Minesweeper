using System;
using System.Collections.Generic;
using UnityEngine;

public class CellViewPool {
    #region Fields

    public event Action<Vector2Int, InputActionType> e_onCellInputEvent;

    private readonly Stack<CellView> m_pool = new Stack<CellView>();
    private readonly Dictionary<Vector2Int, CellView> m_activeCells =
        new Dictionary<Vector2Int, CellView>();

    private readonly CellView m_prefab;
    private readonly Transform m_parent;

    #endregion

    #region Public

    public CellViewPool(CellView a_prefab, Transform a_parent) {
        m_prefab = a_prefab;
        m_parent = a_parent;
    }

    public CellView Get(Vector2Int a_position) {
        CellView cell = GetInternal();

        cell.SetPosition(a_position.x, a_position.y);
        cell.ResetVisual();

        cell.e_onInputEvent += OnCellInput;

        m_activeCells[a_position] = cell;
        return cell;
    }

    public CellView GetByPosition(Vector2Int a_position) {
        if (m_activeCells.TryGetValue(a_position, out CellView cell)) {
            return cell;
        }

        return null;
    }

    public void ReleaseAll() {
        foreach (KeyValuePair<Vector2Int, CellView> pair in m_activeCells) {
            CellView cell = pair.Value;

            cell.e_onInputEvent -= OnCellInput;
            cell.gameObject.SetActive(false);

            m_pool.Push(cell);
        }

        m_activeCells.Clear();
    }

    #endregion

    #region Private

    private CellView GetInternal() {
        CellView cell;

        if (m_pool.Count > 0) {
            cell = m_pool.Pop();
        } else {
            cell = UnityEngine.Object.Instantiate(m_prefab, m_parent);
        }

        cell.gameObject.SetActive(true);
        return cell;
    }

    private void OnCellInput(Vector2Int a_position, InputActionType a_action) {
        e_onCellInputEvent?.Invoke(a_position, a_action);
    }

    #endregion
}
