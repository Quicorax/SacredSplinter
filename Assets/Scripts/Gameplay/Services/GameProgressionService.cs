﻿using System;
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
    [SerializeField] private List<string> _discoveredEnemies = new();
    [SerializeField] private List<int> _completedQuestIndex = new();
    [SerializeField] private List<ProgressionOnLevel> _levelsProgression = new();

    [SerializeField] private int _totalMonstersKilled = 0;

    [SerializeField] private bool _soundOff = false;

    public void Initialize(SaveLoadService saveLoadService) => _saveLoadService = saveLoadService;

    public void LoadInitialResources(GameConfigService config)
    {
        _resources = config.Resources;
        _unlockedHeros = config.UnlockedHeros;
        _levelsProgression = config.LevelsProgression;

        _saveLoadService.Save();
    }

    public void SetAmountOfResource(string resourceId, int elementAmount, bool save = true)
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

    public void SetQuestCompleted(int i)
    {
        _completedQuestIndex.Add(i);
        _saveLoadService.Save();
    }
    public bool CheckQuestCompleted(int i) => _completedQuestIndex.Contains(i);

    public void SetHeroUnlocked(string heroClass)
    {
        _unlockedHeros.Add(heroClass);
        _saveLoadService.Save();
    }
    public bool CheckHeroUnlocked(string heroClass) => _unlockedHeros.Contains(heroClass);
    public void SetEnemyDiscovered(string enemyKind)
    {
        _discoveredEnemies.Add(enemyKind);
        _saveLoadService.Save();
    }
    public bool CheckEnemyDiscovered(string heroClass) => _discoveredEnemies.Contains(heroClass);
    public int CheckAmountOfPregression(string concept)
    {
        switch (concept)
        {
            case "Monster":
                return _totalMonstersKilled;
            case "Location":
                return CheckHigherLevelReached();
            case "Boss_Village":
                return CheckLocationCompleted("Village");
            case "Boss_Sewer":
                return CheckLocationCompleted("Sewers");
            case "Boss_Dungeon":
                return CheckLocationCompleted("Dungeons");
        }

        return 0;
    }

    private int CheckHigherLevelReached()
    {
        int higherLevel = 0;

        foreach (var item in _levelsProgression)
        {
            if (item.MaxLevel > higherLevel)
                higherLevel = item.MaxLevel;
        }

        return higherLevel;
    }

    private int CheckLocationCompleted(string locationName)
    {
        foreach (var item in _levelsProgression)
        {
            if (item.LevelName == locationName && item.Completed)
                return 1;
        }

        return 0;
    }



    public void SetSoundOff(bool off)
    {
        _soundOff = off;
        _saveLoadService.Save();
    }
    public bool CheckSoundOff() => _soundOff;
}