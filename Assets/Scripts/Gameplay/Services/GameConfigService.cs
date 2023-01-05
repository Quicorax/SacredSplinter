﻿using System.Collections.Generic;

public class GameConfigService : IService
{
    public List<ResourceElement> Resources { get; private set; }
    public List<string> UnlockedHeros { get; private set; }
    public List<ProgressionOnLevel> LevelsProgression { get; private set; }


    public void Initialize(InitialResources resources)
    {
        Resources = resources.Resources;
        UnlockedHeros = resources.UnlockedHeros;
        LevelsProgression = resources.LevelsProgression;
    }
}
