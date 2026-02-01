using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StateOfGame : TextCounter {

    #region Fields
    [SerializeField] private Button m_button;
    public UnityAction e_onPressedEvent;
    #endregion


    private void Start() {
        if (m_button) {
            m_button.onClick.AddListener(OnButtonClick);
        }
    }


    private void OnDestroy() {
        if (m_button) {
            m_button.onClick.RemoveListener(OnButtonClick);
        }
    }

    private void OnButtonClick() {
        e_onPressedEvent?.Invoke();
    }
}
