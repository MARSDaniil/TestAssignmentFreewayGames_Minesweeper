using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Zenject;
public class CellView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    #region Fields

    [Inject] private BoardConfig m_boardConfig;

    public UnityAction<Vector2Int, InputActionType> e_onInputEvent;

    [SerializeField] private SpriteRenderer m_spriteRender;
    [SerializeField] private Sprite m_closedStateSprite;
    [SerializeField] private Sprite m_openedStateSprite;
    [SerializeField] private Sprite m_flagStateSprite;
    [SerializeField] private Sprite m_mineStateSprite;
    [SerializeField] private Sprite m_loseGameMineStateSprite;

    [SerializeField] private TMPro.TextMeshProUGUI m_numberText;

    private Vector2Int m_position;
    private float m_pressTime;
    private bool m_pressed;

    #endregion

    #region Public

    public void SetPosition(int a_x, int a_y) {
        m_position = new Vector2Int(a_x, a_y);
    }


    public void SetColorToNumText(Color a_color) {
        m_numberText.color = a_color;
    }

    public void ApplyCellData(BoardCell a_cell) {

        m_numberText.gameObject.SetActive(false);
        m_numberText.text = a_cell.AdjacentMines.ToString();

        if (a_cell.IsOpened) {
            if (a_cell.IsMine) {
                //TODO
                m_spriteRender.sprite = /*a_cell.position == lastOpenedPosition ? m_loseGameMineStateSprite :*/ m_mineStateSprite;
                return;
            }

            m_spriteRender.sprite = m_openedStateSprite;
            if (a_cell.AdjacentMines > 0) {
                m_numberText.gameObject.SetActive(true);
            }

        } else {
            m_spriteRender.sprite = a_cell.IsFlagged ? m_flagStateSprite : m_closedStateSprite;
        }

    }

    #endregion

    #region IPointer

    public void OnPointerDown(PointerEventData a_eventData) {
        m_pressed = true;
        m_pressTime = Time.unscaledTime;

#if UNITY_STANDALONE || UNITY_EDITOR
        if (a_eventData.button == PointerEventData.InputButton.Right) {
            e_onInputEvent?.Invoke(m_position, InputActionType.Flag);
            m_pressed = false;
        }
#endif
    }

    public void OnPointerUp(PointerEventData a_eventData) {
        if (!m_pressed) {
            return;
        }

        float pressDuration = Time.unscaledTime - m_pressTime;
        m_pressed = false;

#if UNITY_STANDALONE || UNITY_EDITOR
        if (a_eventData.button == PointerEventData.InputButton.Left) {
            e_onInputEvent?.Invoke(m_position, InputActionType.Open);
        }
#else
        if (pressDuration >= m_longPressTime)
        {
            e_onInputEvent?.Invoke(m_position, InputActionType.Flag);
        }
        else
        {
            e_onInputEvent?.Invoke(m_position, InputActionType.Open);
        }
#endif
    }

    #endregion
}
