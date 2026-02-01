using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstance : MonoBehaviour {
    #region Fields

    [Inject] private ISceneLoader m_sceneLoader;
    [HideInInspector] public Locale Locale;
    #endregion

    #region UnityEvents

    private void Start() {
        Locale = GetComponentInChildren<Locale>();
        if (!Locale) {
            m_sceneLoader.LoadSingleAsync(Constants.Scene.Lobby);
            Debug.LogError($"Unable to find Locale!", gameObject);
        } else {
        LoadLocale();

        }
    }

    private void LoadLocale() {
        Locale.OnInitDone += OnInitLocale;
        Locale.Init();
    }

    private void OnInitLocale(bool bSuccess) {
        Locale.OnInitDone -= OnInitLocale;
        m_sceneLoader.LoadSingleAsync(Constants.Scene.Lobby);
    }

    #endregion
}

