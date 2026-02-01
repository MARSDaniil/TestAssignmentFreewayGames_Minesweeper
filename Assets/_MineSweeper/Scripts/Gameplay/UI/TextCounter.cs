using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextCounter : MonoBehaviour
{
    #region Fields
    [SerializeField] private TMP_Text m_text;
    #endregion

    #region Public

    public void SetText(string a_text) {
        m_text.text = a_text;
    }

    #endregion
}
