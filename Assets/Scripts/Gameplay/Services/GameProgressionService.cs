using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ResourceElement
{
    public string Key;
    public int Amount;
}

[Serializable]
public class GameProgressionService : IService
{
    private SaveLoadService _saveLoadService;

    [SerializeField] private List<ResourceElement> _resources = new();
    [SerializeField] private List<string> _unlockedHeros = new();
    [SerializeField] private List<int> _completedQuestIndex = new();
    [SerializeField] private List<ProgressionOnLevel> _levelsProgression = new();

    [SerializeField] private int _totalMonstersKilled = 0;

    [SerializeField] private bool _sfxOff, _musicOff = false;

    public void Initialize(SaveLoadService saveLoadService) => _saveLoadService = saveLoadService;

    public void LoadInitialResources(GameConfigService config)
    {
        _resources = config.Resources;
        _unlockedHeros = config.UnlockedHeros;
        _completedQuestIndex = config.CompletedQuestIndex;
        _levelsProgression = config.LevelsProgression;

        _saveLoadService.Save();
    }

    public void SetAmoutOfResource(string resourceId, int elementAmount, bool save = true)
    {
        foreach (var resourcePack in _resources)
        {
            if (resourcePack.Key == resourceId)
                resourcePack.Amount += elementAmount;
        }

        if (save)
            _saveLoadService.Save();
    }
    public int CheckAmountOfResource(string resourceId)
    {
        int amount = -1;

        foreach (var resourcePack in _resources)
        {
            if (resourcePack.Key == resourceId)
            {
                amount = resourcePack.Amount;
                break;
            }
        }

        return amount;
    }

    public void SetQuestCompleted(int i) => _completedQuestIndex.Add(i);
    public bool GetQuestCompleted(int i) => _completedQuestIndex.Contains(i);

    public int GetAmountOfPregression(string concept)
    {
        switch (concept)
        {
            case "Monster":
                return _totalMonstersKilled;
            case "Dungeon":
                return GetHigherLevelReached();
            case "Boss_Village":
                return GetLocationCompleted("Village");
            case "Boss_Sewer":
                return GetLocationCompleted("Sewers");
            case "Boss_Dungeon":
                return GetLocationCompleted("Dungeons");
        }
    
        return 0;
    }
    private int GetHigherLevelReached()
    {
        int higherLevel = 0;
    
        foreach (var item in _levelsProgression)
        {
            if (item.MaxLevel > higherLevel)
                higherLevel = item.MaxLevel;
        }
    
        return higherLevel;
    }
    
    private int GetLocationCompleted(string locationName)
    {
        foreach (var item in _levelsProgression)
        {
            if (item.LevelName == locationName && item.Completed)
                return 1;
        }
    
        return 0;
    }
    public void SetSFXOff(bool off)
    {
        _sfxOff = off;
        _saveLoadService.Save();
    }
    public void SetMusicOff(bool off)
    {
        _musicOff = off;
        _saveLoadService.Save();
    }
    public bool CheckSFXOff() => _sfxOff;
    public bool CheckMusicOff() => _musicOff;
}
