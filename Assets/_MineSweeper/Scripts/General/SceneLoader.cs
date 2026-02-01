using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
public sealed class SceneLoader : ISceneLoader {
    #region Fields

    public event Action<SceneLoadState> e_onStateChangedEvent;

    public bool IsLoading {
        get; private set;
    }
    public string CurrentSceneName {
        get; private set;
    }

    #endregion

    #region Public

    public async Task LoadSingleAsync(string a_sceneName, CancellationToken a_ct = default) {
        await LoadAsyncInternal(a_sceneName, LoadSceneMode.Single, a_ct);
    }

    public async Task LoadAdditiveAsync(string a_sceneName, CancellationToken a_ct = default) {
        await LoadAsyncInternal(a_sceneName, LoadSceneMode.Additive, a_ct);
    }

    public async Task UnloadAsync(string a_sceneName, CancellationToken a_ct = default) {
        a_ct.ThrowIfCancellationRequested();

        Scene scene = SceneManager.GetSceneByName(a_sceneName);
        if (!scene.isLoaded) {
            return;
        }

        if (IsLoading) {
            throw new InvalidOperationException("SceneLoader is busy.");
        }

        IsLoading = true;
        RaiseState(a_sceneName, 0f, "unloading");

        AsyncOperation op = SceneManager.UnloadSceneAsync(a_sceneName);
        if (op == null) {
            IsLoading = false;
            RaiseState(a_sceneName, 1f, "unload_failed");
            return;
        }

        while (!op.isDone) {
            a_ct.ThrowIfCancellationRequested();
            RaiseState(a_sceneName, op.progress, "unloading");
            await Task.Yield();
        }

        IsLoading = false;
        RaiseState(a_sceneName, 1f, "unloaded");
    }

    #endregion

    #region Private

    private async Task LoadAsyncInternal(string a_sceneName, LoadSceneMode a_mode, CancellationToken a_ct) {
        a_ct.ThrowIfCancellationRequested();

        if (IsLoading) {
            throw new InvalidOperationException("SceneLoader is busy.");
        }

        IsLoading = true;
        RaiseState(a_sceneName, 0f, "loading");

        AsyncOperation op = SceneManager.LoadSceneAsync(a_sceneName, a_mode);
        if (op == null) {
            IsLoading = false;
            RaiseState(a_sceneName, 1f, "load_failed");
            return;
        }

        op.allowSceneActivation = false;

        while (op.progress < 0.9f) {
            a_ct.ThrowIfCancellationRequested();

            float normalizedProgress = Mathf.Clamp01(op.progress / 0.9f);
            RaiseState(a_sceneName, normalizedProgress, "loading");

            await Task.Yield();
        }

        RaiseState(a_sceneName, 1f, "activating");

        await Task.Yield();

        a_ct.ThrowIfCancellationRequested();
        op.allowSceneActivation = true;

        while (!op.isDone) {
            a_ct.ThrowIfCancellationRequested();
            RaiseState(a_sceneName, 1f, "activating");
            await Task.Yield();
        }

        IsLoading = false;
        CurrentSceneName = a_sceneName;
        RaiseState(a_sceneName, 1f, "loaded");
    }

    private void RaiseState(string a_sceneName, float a_progress, string a_phase) {
        e_onStateChangedEvent?.Invoke(new SceneLoadState(IsLoading, a_sceneName, a_progress, a_phase));
    }

    #endregion
}
