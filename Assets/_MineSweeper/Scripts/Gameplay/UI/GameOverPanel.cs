using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour {
    #region Fields

    public UnityAction e_onRestartPressedEvent;
    public UnityAction e_onBackToMenuPressedEvent;

    [SerializeField] private Button m_restartButton;
    [SerializeField] private Button m_backToMenuButton;
    [SerializeField] private TMP_Text m_title;

    [SerializeField] private CanvasGroup m_canvasGroup;

    private bool m_isShowed = true;

    #endregion

    #region UnityEvents

    private void Awake() {
        if (m_restartButton != null) {
            m_restartButton.onClick.AddListener(OnRestartPressed);
        }

        if (m_backToMenuButton != null) {
            m_backToMenuButton.onClick.AddListener(OnBackToMenuPressed);
        }
    }

    private void OnDestroy() {
        if (m_restartButton != null) {
            m_restartButton.onClick.RemoveListener(OnRestartPressed);
        }

        if (m_backToMenuButton != null) {
            m_backToMenuButton.onClick.RemoveListener(OnBackToMenuPressed);
        }
    }

    #endregion

    #region Public

    public void SetResult(string a_result) {
        m_title.text = a_result;
    }

    public void Show() {
        if (m_isShowed) {
            return;
        }

        m_isShowed = true;
    }

    public void Hide(bool a_immediate = false) {
        if (!m_isShowed) {
            return;
        }

        m_isShowed = false;
    }

    #endregion

    #region Private

    private void OnRestartPressed() {
        e_onRestartPressedEvent?.Invoke();
    }

    private void OnBackToMenuPressed() {
        e_onBackToMenuPressedEvent?.Invoke();
    }

    #endregion
}
