﻿using System;
using System.Threading.Tasks;

namespace Quicorax.SacredSplinter.Services
{
    public class ServiceFeeder
    {
        const string _enviroment = "development";
        //const string _enviroment = "production";

        public async Task LoadServices(ServiceElements elements, Action<string> onElementLoaded)
        {
            var servicesInitializer = new ServicesInitializer(_enviroment);

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

            ServiceLocator.RegisterService(gameConfig);
            ServiceLocator.RegisterService(gameProgression);
            ServiceLocator.RegisterService(saveLoad);
            ServiceLocator.RegisterService(navigation);
            ServiceLocator.RegisterService(popUpSpawner);
            ServiceLocator.RegisterService(adventureConfig);
            ServiceLocator.RegisterService(adventureProgress);
            ServiceLocator.RegisterService(addressables);

            onElementLoaded.Invoke("Sharpening Swords");
            await servicesInitializer.Initialize();
            onElementLoaded.Invoke("Setting Artifacts");
            await loginService.Initialize();
            onElementLoaded.Invoke("Invoking Monsters");
            await remoteConfig.Initialize();
            onElementLoaded.Invoke("Defining Relics");
            await gameProgressionProvider.Initialize();
            onElementLoaded.Invoke("Calling Warriors");
            await addressables.Initialize(elements.AssetsToPrewarm.Assets);
            onElementLoaded.Invoke("Casting Spells");
            
            gameConfig.Initialize(remoteConfig);
            gameProgression.Initialize(saveLoad);
            saveLoad.Initialize(gameConfig, gameProgression, gameProgressionProvider);
            
            popUpSpawner.Initialize(elements.PopUpTransformParent);
            adventureProgress.Initialize(gameProgression, elements.OnPlayerDeath);
        }
    }
}