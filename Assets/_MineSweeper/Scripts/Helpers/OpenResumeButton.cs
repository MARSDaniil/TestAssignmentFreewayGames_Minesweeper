using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenResumeButton : MonoBehaviour
{
    [SerializeField] private Button m_openLinkButton;
    private const string CV = "https://drive.google.com/file/d/16cX2ejxn8BW3ykngdfO3J7DBIJxr6FG9/view?usp=sharing";
    void Start()
    {
        m_openLinkButton.onClick.AddListener(OpenLink);
    }
    private void OnDestroy() {
        m_openLinkButton.onClick.RemoveListener(OpenLink);
    }

    private void OpenLink() {
        Application.OpenURL(CV);
    }
}
