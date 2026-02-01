using System;
using System.Threading;
using System.Threading.Tasks;
public interface ISceneLoader {
    #region Public

    event Action<SceneLoadState> e_onStateChangedEvent;

    bool IsLoading {
        get;
    }
    string CurrentSceneName {
        get;
    }

    Task LoadSingleAsync(string a_sceneName, CancellationToken a_ct = default);
    Task LoadAdditiveAsync(string a_sceneName, CancellationToken a_ct = default);
    Task UnloadAsync(string a_sceneName, CancellationToken a_ct = default);

    #endregion
}

public readonly struct SceneLoadState {
    #region Public

    public readonly bool IsLoading;
    public readonly string SceneName;
    public readonly float Progress;
    public readonly string Phase;

    public SceneLoadState(bool a_isLoading, string a_sceneName, float a_progress, string a_phase) {
        IsLoading = a_isLoading;
        SceneName = a_sceneName;
        Progress = a_progress;
        Phase = a_phase;
    }

    #endregion
}
