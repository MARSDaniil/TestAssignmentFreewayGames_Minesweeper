using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class LobbyStartButtonsPanel : MonoBehaviour {

    #region Fields

    public UnityAction e_onPlayPressedEvent;

    [SerializeField] private Button m_playButton;
    [SerializeField] private SelectButton m_bombCount;
    [SerializeField] private SelectButton m_sizeCount;

    public SelectButton bombCount {
        get {
            return m_bombCount;
        }
    }

    public SelectButton sizeCount {
        get {
            return m_sizeCount;
        }
    }
    #endregion

    #region UnityEvents

    private void Awake() {
        if (m_playButton != null) {
            m_playButton.onClick.AddListener(OnPlayPressed);
        }

    }

    private void OnDestroy() {
        if (m_playButton != null) {
            m_playButton.onClick.RemoveListener(OnPlayPressed);
        }
     
    }

    #endregion

    #region Private

    private void OnPlayPressed() {
        e_onPlayPressedEvent?.Invoke();
    }

    #endregion
}
