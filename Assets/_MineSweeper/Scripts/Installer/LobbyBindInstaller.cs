using Zenject;
using UnityEngine;
public class LobbyBindInstaller : MonoInstaller {

    [SerializeField] private LobbyUIController m_lobbyUIController;
    
    public override void InstallBindings() {
        Container.Bind<LobbyUIController>().FromInstance(m_lobbyUIController).AsSingle();
        Container.BindInterfacesTo<LobbyController>().AsSingle();
        Container.BindInterfacesTo<LobbySettingsPresenter>().AsSingle();
    }
}
