using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class GameOverPanel : MonoBehaviour {
    #region Fields

    public UnityAction e_onRestartPressedEvent;
    public UnityAction e_onBackToMenuPressedEvent;

    [SerializeField] private Button m_restartButton;
    [SerializeField] private Button m_backToMenuButton;
    [SerializeField] private TMP_Text m_title;
    [SerializeField] private CanvasGroup m_canvasGroup;

    [SerializeField] private float m_fadeDuration = 0.25f;

    private bool m_isShowed;
    private Tween m_fadeTween;

    #endregion

    #region UnityEvents

    private void Awake() {
        if (m_restartButton != null) {
            m_restartButton.onClick.AddListener(OnRestartPressed);
        }

        if (m_backToMenuButton != null) {
            m_backToMenuButton.onClick.AddListener(OnBackToMenuPressed);
        }

        ForceHidden();
    }

    private void OnDestroy() {
        if (m_restartButton != null) {
            m_restartButton.onClick.RemoveListener(OnRestartPressed);
        }

        if (m_backToMenuButton != null) {
            m_backToMenuButton.onClick.RemoveListener(OnBackToMenuPressed);
        }

        KillTween();
    }

    #endregion

    #region Public

    public void SetResult(string a_result) {
        if (m_title != null) {
            m_title.text = a_result;
        }
    }

    public void Show() {
        if (m_isShowed) {
            return;
        }

        m_isShowed = true;

        gameObject.SetActive(true);
        KillTween();

        m_canvasGroup.alpha = 0f;
        m_canvasGroup.interactable = false;
        m_canvasGroup.blocksRaycasts = true;

        m_fadeTween = m_canvasGroup
            .DOFade(1f, m_fadeDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => {
                m_canvasGroup.interactable = true;
            });
    }

    public void Hide(bool a_immediate = false) {
        if (!m_isShowed) {
            return;
        }

        m_isShowed = false;

        KillTween();

        if (a_immediate) {
            ForceHidden();
            return;
        }

        m_canvasGroup.interactable = false;

        m_fadeTween = m_canvasGroup
            .DOFade(0f, m_fadeDuration)
            .SetEase(Ease.InQuad)
            .OnComplete(() => {
                gameObject.SetActive(false);
                m_canvasGroup.blocksRaycasts = false;
            });
    }

    #endregion

    #region Private

    private void ForceHidden() {
        KillTween();

        m_isShowed = false;

        if (m_canvasGroup != null) {
            m_canvasGroup.alpha = 0f;
            m_canvasGroup.interactable = false;
            m_canvasGroup.blocksRaycasts = false;
        }

        gameObject.SetActive(false);
    }

    private void KillTween() {
        if (m_fadeTween != null && m_fadeTween.IsActive()) {
            m_fadeTween.Kill();
            m_fadeTween = null;
        }
    }

    private void OnRestartPressed() {
        e_onRestartPressedEvent?.Invoke();
    }

    private void OnBackToMenuPressed() {
        e_onBackToMenuPressedEvent?.Invoke();
    }

    #endregion
}
