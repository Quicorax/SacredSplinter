using System;
using UnityEngine;

public class ServiceFeeder
{
    public void LoadSevices(ServiceElements elements)
    {
        GameConfigService gameConfig = new();
        GameProgressionService gameProgression = new();
        SaveLoadService saveLoad = new();

        NavigationService navigation = new();
        PopUpSpawnerService popUpSpawner = new();

        ServiceLocator.RegisterService(gameConfig);
        ServiceLocator.RegisterService(gameProgression);
        ServiceLocator.RegisterService(saveLoad);

        ServiceLocator.RegisterService(navigation);
        ServiceLocator.RegisterService(popUpSpawner);

        gameConfig.Initialize(elements.initialResources);
        gameProgression.Initialize(saveLoad);
        saveLoad.Initialize(gameConfig, gameProgression);

        popUpSpawner.Initialize(elements.popUpTransformParent);
    }
}