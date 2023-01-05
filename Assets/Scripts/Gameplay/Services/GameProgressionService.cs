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
        _resources = config.InitialResources;
        _unlockedHeros = config.UnlockedHeros;
        _completedQuestIndex = config.CompletedQuestIndex;
        _levelsProgression = config.LevelsProgression;

        _saveLoadService.Save();
    }

    public void UpdateElement(string resourceId, int elementAmount, bool save = true)
    {
        foreach (var resourcePack in _resources)
        {
            if (resourcePack.Key == resourceId)
                resourcePack.Amount += elementAmount;
        }

        if (save)
            _saveLoadService.Save();
    }
    public int CheckElement(string resourceId)
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
