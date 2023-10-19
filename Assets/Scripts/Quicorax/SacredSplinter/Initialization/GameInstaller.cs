using Zenject;

namespace Quicorax.SacredSplinter.Services
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IServicesInitializer>().To<ServicesInitializer>().AsSingle();
            Container.Bind<ILoginGameService>().To<LoginGameService>().AsSingle();
            Container.Bind<IRemoteConfigService>().To<RemoteConfigService>().AsSingle();
            Container.Bind<IGameConfigService>().To<GameConfigService>().AsSingle();
            Container.Bind<IGameProgressionService>().To<GameProgressionService>().AsSingle();
            Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
            Container.Bind<INavigationService>().To<NavigationService>().AsSingle();
            Container.Bind<IPopUpSpawnerService>().To<PopUpSpawnerService>().AsSingle();
            Container.Bind<IAdventureConfigurationService>().To<AdventureConfigurationService>().AsSingle();
            Container.Bind<IAdventureProgressionService>().To<AdventureProgressionService>().AsSingle();
            Container.Bind<IGameProgressionProvider>().To<GameProgressionProvider>().AsSingle();
            Container.Bind<IAddressablesService>().To<AddressablesService>().AsSingle();
        }
    }
}