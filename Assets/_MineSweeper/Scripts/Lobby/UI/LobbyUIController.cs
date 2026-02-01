using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyUIController : MonoBehaviour {

    [SerializeField] private LobbyStartButtonsPanel m_lobbyStartButtonsPanel;
    public LobbyStartButtonsPanel lobbyStartButtonsPanel {
        get {
            return m_lobbyStartButtonsPanel;
        }
    }
}
