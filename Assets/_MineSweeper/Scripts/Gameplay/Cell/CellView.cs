using UnityEngine;
using UnityEngine.Events;

public class CellView : MonoBehaviour {
    #region Fields

    public UnityAction<Vector2Int> e_onLeftClickEvent;
    public UnityAction<Vector2Int> e_onRightClickEvent;

    [SerializeField] private GameObject m_closedState;
    [SerializeField] private GameObject m_flagState;
    [SerializeField] private GameObject m_mineState;
    [SerializeField] private TMPro.TextMeshProUGUI m_numberText;

    private Vector2Int m_position;

    #endregion

    #region Public

    public void SetPosition(int a_x, int a_y) {
        m_position = new Vector2Int(a_x, a_y);
    }

    public void ResetVisual() {
        m_closedState.SetActive(true);
        m_flagState.SetActive(false);
        m_mineState.SetActive(false);
        m_numberText.gameObject.SetActive(false);
    }

    public void ApplyCellData(BoardCell a_cell) {
        m_closedState.SetActive(!a_cell.IsOpened);
        m_flagState.SetActive(a_cell.IsFlagged);

        if (!a_cell.IsOpened) {
            return;
        }

        if (a_cell.IsMine) {
            m_mineState.SetActive(true);
            return;
        }

        if (a_cell.AdjacentMines > 0) {
            m_numberText.text = a_cell.AdjacentMines.ToString();
            m_numberText.gameObject.SetActive(true);
        }
    }

    #endregion

    #region UnityEvents

    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(0)) {
            e_onLeftClickEvent?.Invoke(m_position);
        }

        if (Input.GetMouseButtonDown(1)) {
            e_onRightClickEvent?.Invoke(m_position);
        }
    }

    #endregion
}
