using System;
using System.Threading.Tasks;

namespace Quicorax.SacredSplinter.Services
{
    public class ServiceFeeder
    {
        //const string _enviroment = "development";
        const string _enviroment = "production";
        
        private Action<string> _onElementLoaded;

        public async Task LoadServices(ServiceElements elements, Action<string> onElementLoaded)
        {
            _onElementLoaded = onElementLoaded;
            
            var servicesInitializer = new ServicesInitializer();
            var loginService = new LoginGameService();
            var remoteConfig = new RemoteConfigService();
            var gameConfig = new GameConfigService();
            var gameProgression = new GameProgressionService();
            var saveLoad = new SaveLoadService();
            var navigation = new NavigationService();
            var popUpSpawner = new PopUpSpawnerService();
            var adventureConfig = new AdventureConfigurationService();
            var adventureProgress = new AdventureProgressionService();
            var gameProgressionProvider = new GameProgressionProvider();
            var addressables = new AddressablesService();

            _onElementLoaded.Invoke("Sharpening Swords");
            await servicesInitializer.Initialize(_enviroment);
           _onElementLoaded.Invoke("Setting Artifacts");
            await loginService.Initialize();
           _onElementLoaded.Invoke("Invoking Monsters");
            await remoteConfig.Initialize();
           _onElementLoaded.Invoke("Defining Relics");
            await gameProgressionProvider.Initialize();
           _onElementLoaded.Invoke("Calling Warriors");
            await addressables.Initialize(elements.AssetsToPrewarm.Assets);
           _onElementLoaded.Invoke("Casting Spells");
            
            gameConfig.Initialize(remoteConfig);
            gameProgression.Initialize(saveLoad);
            saveLoad.Initialize(gameConfig, gameProgression, gameProgressionProvider);
            
            popUpSpawner.Initialize(elements.PopUpTransformParent);
            adventureProgress.Initialize(gameProgression, elements.OnPlayerDeath);
        }
    }
}