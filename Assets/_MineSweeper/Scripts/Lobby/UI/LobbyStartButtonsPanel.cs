using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class LobbyStartButtonsPanel : MonoBehaviour {

    #region Fields

    public UnityAction e_onPlayPressedEvent;
    public UnityAction e_onContinuePressedEvent;

    [SerializeField] private Button m_playButton;
    [SerializeField] private Button m_continueButton;

    #endregion

    #region UnityEvents

    private void Awake() {
        if (m_playButton != null) {
            m_playButton.onClick.AddListener(OnPlayPressed);
        }

        if (m_continueButton != null) {
            m_continueButton.onClick.AddListener(OnContinuePressed);
        }
    }

    private void OnDestroy() {
        if (m_playButton != null) {
            m_playButton.onClick.RemoveListener(OnPlayPressed);
        }

        if (m_continueButton != null) {
            m_continueButton.onClick.RemoveListener(OnContinuePressed);
        }
    }

    #endregion

    #region Public

    public void SetContinueAvailable(bool a_value) {
        if (m_continueButton != null) {
            m_continueButton.interactable = a_value;
        }
    }

    #endregion

    #region Private

    private void OnPlayPressed() {
        e_onPlayPressedEvent?.Invoke();
    }

    private void OnContinuePressed() {
        e_onContinuePressedEvent?.Invoke();
    }

    #endregion
}
