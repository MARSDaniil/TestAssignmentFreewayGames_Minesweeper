using System;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class GameInputController : ITickable {
    #region Fields

    public event UnityAction e_onRestartPressedEvent;

    private bool m_inputBlocked;

    #endregion

    #region Public

    public void BlockInput() {
        m_inputBlocked = true;
    }

    public void Tick() {
        if (m_inputBlocked) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            e_onRestartPressedEvent?.Invoke();
        }
    }

    #endregion
}
