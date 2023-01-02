using Quicorax;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ProgressionOnLevel
{
    public string LevelName;
    public int MaxLevel;
    public bool Completed;
}

[CreateAssetMenu(menuName = "Quicorax/Data/UserModel")]
public class UserModel : ScriptableObject
{
    [SerializeField]
    private SimpleEventBus _onResourcesUpdated;

    [SerializeField]
    private List<Reward> _resources = new();
    [SerializeField]
    private List<string> _heros = new();

    [SerializeField]
    private ProgressionOnLevel[] _levelProgression = new ProgressionOnLevel[3];
    [SerializeField]
    private int _monstersKilled;

    public int GetAmountOfResource(string kind)
    {
        foreach (Reward reward in _resources)
        {
            if (reward.Item.Name == kind)
                return reward.Amount;
        }

        return 0;
    }

    public void SetAmoutOfItem(string kind, int amount)
    {
        foreach (Reward reward in _resources)
        {
            if (reward.Item.Name == kind)
                reward.Amount += amount;
        }

        _onResourcesUpdated.NotifyEvent();
    }

    public void SetMonterKilled() => _monstersKilled++;
    public int GetKilledMonsters() => _monstersKilled;
    public int GetAmountOfPregression(string concept) 
    {
        switch (concept)
        {
            case "Monster":
                return _monstersKilled;
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

        foreach (var item in _levelProgression)
        {
            if (item.MaxLevel > higherLevel)
                higherLevel = item.MaxLevel;
        }

        return higherLevel;
    }

    private int GetLocationCompleted(string locationName)
    {
        foreach (var item in _levelProgression)
        {
            if (item.LevelName == locationName && item.Completed)
                return 1;
        }

        return 0;
    }

    public void UnlockHeroClass(string heroClass)
    {
        if (!CheckHeroUnlocked(heroClass))
            _heros.Add(heroClass);
    }

    public bool CheckHeroUnlocked(string heroClass) => _heros.Contains(heroClass);
}
