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
            var viewElements = new ElementImages();
            var navigation = new NavigationService();
            var popUpSpawner = new PopUpSpawnerService();
            var adventure = new AdventureConfigurationService();

            ServiceLocator.RegisterService(gameConfig);
            ServiceLocator.RegisterService(gameProgression);
            ServiceLocator.RegisterService(saveLoad);
            ServiceLocator.RegisterService(navigation);
            ServiceLocator.RegisterService(popUpSpawner);
            ServiceLocator.RegisterService(adventure);
            ServiceLocator.RegisterService(models);
            ServiceLocator.RegisterService(viewElements);

            gameConfig.Initialize(elements.InitialResources);
            gameProgression.Initialize(saveLoad);
            saveLoad.Initialize(gameConfig, gameProgression);
            popUpSpawner.Initialize(elements.PopUpTransformParent);
            models.Initialize();
            viewElements.Initialize(elements.ViewElements);
        }
    }
}