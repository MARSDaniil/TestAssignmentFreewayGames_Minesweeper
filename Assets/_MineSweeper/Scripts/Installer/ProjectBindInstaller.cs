using UnityEngine;
using Zenject;

public class ProjectBindInstaller : MonoInstaller {
    [SerializeField] private GameInstance m_gameInstance;
    [SerializeField] private BoardConfig m_boardConfig;
    public override void InstallBindings() {
        Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
        Container.Bind<GameInstance>().FromInstance(m_gameInstance).AsSingle();
        Container.Bind<BoardConfig>().FromInstance(m_boardConfig).AsSingle();
    }
}
