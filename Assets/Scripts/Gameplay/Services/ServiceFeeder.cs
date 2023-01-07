public class ServiceFeeder
{
    public void LoadSevices(ServiceElements elements)
    {
        GameConfigService gameConfig = new();
        GameProgressionService gameProgression = new();
        SaveLoadService saveLoad = new();
        ModelsService models = new();
        ImagesService viewElements = new();

        NavigationService navigation = new();
        PopUpSpawnerService popUpSpawner = new();
        AdventureConfigurationService adventure = new();

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
