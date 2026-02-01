using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstance : MonoBehaviour {
    #region Fields

    [Inject] private ISceneLoader m_sceneLoader;

    #endregion

    #region UnityEvents

    private void Start() {
        m_sceneLoader.LoadSingleAsync(Constants.Scene.Lobby);
    }

    #endregion
}

