using Zenject;

namespace Quicorax.SacredSplinter.Services
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IServicesInitializer>().To<ServicesInitializer>().AsSingle().NonLazy();
            Container.Bind<ILoginGameService>().To<LoginGameService>().AsSingle().NonLazy();
            Container.Bind<IRemoteConfigService>().To<RemoteConfigService>().AsSingle().NonLazy();
            Container.Bind<IGameConfigService>().To<GameConfigService>().AsSingle().NonLazy();
            Container.Bind<IGameProgressionService>().To<GameProgressionService>().AsSingle().NonLazy();
            Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle().NonLazy();
            Container.Bind<INavigationService>().To<NavigationService>().AsSingle().NonLazy();
            Container.Bind<IPopUpSpawnerService>().To<PopUpSpawnerService>().AsSingle().NonLazy();
            Container.Bind<IAdventureConfigurationService>().To<AdventureConfigurationService>().AsSingle().NonLazy();
            Container.Bind<IAdventureProgressionService>().To<AdventureProgressionService>().AsSingle().NonLazy();
            Container.Bind<IGameProgressionProvider>().To<GameProgressionProvider>().AsSingle().NonLazy();
            Container.Bind<IAddressablesService>().To<AddressablesService>().AsSingle().NonLazy();
        }
    }
}