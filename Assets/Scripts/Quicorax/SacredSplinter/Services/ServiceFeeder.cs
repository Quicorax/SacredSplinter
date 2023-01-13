namespace Quicorax.SacredSplinter.Services
{
    public class ServiceFeeder
    {
        public static void LoadServices(ServiceElements elements)
        {
            var gameConfig = new GameConfigService();
            var gameProgression = new GameProgressionService();
            var saveLoad = new SaveLoadService();
            var models = new ModelsService();
            var viewElements = new ElementImagesService();
            var navigation = new NavigationService();
            var popUpSpawner = new PopUpSpawnerService();
            var adventureConfig = new AdventureConfigurationService();
            var adventureProgress = new AdventureProgressionService();

            ServiceLocator.RegisterService(gameConfig);
            ServiceLocator.RegisterService(gameProgression);
            ServiceLocator.RegisterService(saveLoad);
            ServiceLocator.RegisterService(navigation);
            ServiceLocator.RegisterService(popUpSpawner);
            ServiceLocator.RegisterService(adventureConfig);
            ServiceLocator.RegisterService(models);
            ServiceLocator.RegisterService(viewElements);
            ServiceLocator.RegisterService(adventureProgress);

            gameConfig.Initialize(elements.InitialResources);
            gameProgression.Initialize(saveLoad);
            saveLoad.Initialize(gameConfig, gameProgression);
            popUpSpawner.Initialize(elements.PopUpTransformParent);
            models.Initialize();
            viewElements.Initialize(elements.ViewElements);
            adventureProgress.Initialize(gameProgression, elements.OnPlayerDeath);
        }
    }
}