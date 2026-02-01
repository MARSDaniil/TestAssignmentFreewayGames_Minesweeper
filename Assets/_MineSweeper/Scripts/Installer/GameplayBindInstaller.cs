using UnityEngine;
using Zenject;

public class GameplayBindInstaller : MonoInstaller {
    #region Fields

    [SerializeField] private BoardView m_boardView;
    [SerializeField] private GameplayUIController m_gameplayUIController;
    [SerializeField] private BoardConfig m_boardConfig;
    #endregion

    #region Public

    public override void InstallBindings() {
        Container.Bind<BoardConfig>().FromInstance(m_boardConfig).AsSingle();
        Container.BindInterfacesTo<BoardService>().AsSingle();
        Container.BindInterfacesTo<GameFlowController>().AsSingle();
        Container.BindInterfacesTo<BoardPresenter>().AsSingle();
        Container.BindInterfacesTo<GameInputController>().AsSingle();

        Container.Bind<BoardView>().FromInstance(m_boardView).AsSingle();
        Container.Bind<GameplayUIController>().FromInstance(m_gameplayUIController).AsSingle();

        Container.Bind<CellViewPool>().AsSingle();
    }

    #endregion
}
