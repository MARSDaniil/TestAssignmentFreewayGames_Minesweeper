using UnityEngine;
using Zenject;

public class GameplayBindInstaller : MonoInstaller {
    #region Fields

    [SerializeField] private BoardView m_boardView;
    [SerializeField] private BoardConfig m_boardConfig;
    [SerializeField] private GameplayUIController m_gameplayUIController;

    [SerializeField] private Camera m_mainCamera;
    [SerializeField] private float m_cameraPadding = 0.5f;

    #endregion

    #region Public

    public override void InstallBindings() {
        Container.Bind<BoardConfig>().FromInstance(m_boardConfig).AsSingle();

        Container.Bind<BoardView>().FromInstance(m_boardView).AsSingle();
        Container.Bind<GameplayUIController>().FromInstance(m_gameplayUIController).AsSingle();

        Container.Bind<CellViewPool>()
            .AsSingle()
            .WithArguments(m_boardView.CellPrefab, m_boardView.CellsRoot);

        Container.BindInterfacesTo<BoardService>().AsSingle();

        Container.BindInterfacesAndSelfTo<BoardPresenter>().AsSingle();
        Container.BindInterfacesTo<GameInputRouter>().AsSingle();

        Container.Bind<GameInputController>().AsSingle();
        Container.BindInterfacesAndSelfTo<HudPresenter>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameFlowController>().AsSingle();
        Container.BindInterfacesTo<GameOverPresenter>().AsSingle();
        Container.Bind<BoardCameraController>().AsSingle().WithArguments(m_mainCamera, m_boardView, m_cameraPadding);
    }

    #endregion
}
