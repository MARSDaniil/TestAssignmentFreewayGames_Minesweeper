using UnityEngine;
using UnityEngine.UI;

public class GameplayUIController : MonoBehaviour {
    #region Fields

    [SerializeField] private StateOfGame m_stateOfGames;
    public StateOfGame stateOfGames {
        get {
            return m_stateOfGames;
        }
    }
    [SerializeField] private TextCounter m_flagCounter;
    [SerializeField] private TextCounter m_timer;

    #endregion

    #region Public

    public void SetFlagsLeft(int a_value) {
        if (m_flagCounter == null) {
            return;
        }

        int safe = Mathf.Max(0, a_value);
        m_flagCounter.SetText(safe.ToString());
    }

    public void SetTimerSeconds(int a_seconds) {
        if (m_timer == null) {
            return;
        }

        int safe = Mathf.Max(0, a_seconds);
        m_timer.SetText(safe.ToString());
    }

    public void SetFaceNormal() {
        if (m_stateOfGames == null) {
            return;
        }
        //TODO
        m_stateOfGames.SetText("😊");
    }

    public void ShowWin() {
        if (m_stateOfGames == null) {
            return;
        }
        //TODO
        m_stateOfGames.SetText("😍");
    }

    public void ShowLose() {
        if (m_stateOfGames == null) {
            return;
        }
        //TODO
        m_stateOfGames.SetText("☹");
    }


    #endregion
}

