using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour {
    #region Fields

    public UnityAction e_onReduceEvent;
    public UnityAction e_onIncreaseEvent;

    [SerializeField] private TMP_Text m_counter;
    [SerializeField] private Button m_leftButton;
    [SerializeField] private Button m_rightButton;

    #endregion

    #region UnityEvents

    private void Awake() {
        if (m_leftButton != null) {
            m_leftButton.onClick.AddListener(OnReducePressed);
        }

        if (m_rightButton != null) {
            m_rightButton.onClick.AddListener(OnIncreasePressed);
        }
    }

    private void OnDestroy() {
        if (m_leftButton != null) {
            m_leftButton.onClick.RemoveListener(OnReducePressed);
        }

        if (m_rightButton != null) {
            m_rightButton.onClick.RemoveListener(OnIncreasePressed);
        }
    }

    #endregion

    #region Public

    public void SetCounter(string a_value) {
        if (m_counter == null) {
            return;
        }

        m_counter.text = a_value;
    }

    #endregion

    #region Private

    private void OnReducePressed() {
        e_onReduceEvent?.Invoke();
    }

    private void OnIncreasePressed() {
        e_onIncreaseEvent?.Invoke();
    }

    #endregion
}
