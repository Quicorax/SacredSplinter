using System;
using System.Collections.Generic;
using System.Linq;
using Quicorax.SacredSplinter.Models;
using UnityEngine;

namespace Quicorax.SacredSplinter.Services
{
    [Serializable]
    public class GameProgressionService : IService
    {
        private SaveLoadService _saveLoadService;

        [SerializeField] private int _ticksPlayed = 0;

        [SerializeField] private List<ResourceElement> _resources = new();
        [SerializeField] private List<ProgressionOnLevel> _levelsProgression = new();

        [SerializeField] private List<string> _unlockedHeros = new();
        [SerializeField] private List<string> _discoveredEnemies = new();

        [SerializeField] private List<int> _completedQuestIndex = new();

        [SerializeField] private int _totalMonstersKilled = 0;
        [SerializeField] private int _totalRoomsCleared = 0;

        [SerializeField] private bool _soundOn = true;

        private Dictionary<string, ResourceElement> _sortedResources = new();
        private Dictionary<string, ProgressionOnLevel> _sortedLevelsProgression = new();

        public void Initialize(SaveLoadService saveLoadService) => _saveLoadService = saveLoadService;

        public void DeserializeModels()
        {
            DeserializeResources(_resources);

            foreach (var level in _levelsProgression)
                _sortedLevelsProgression.Add(level.LevelName, level);
        }

        public void SerializeModels(Action onComplete)
        {
            _resources.Clear();
            _levelsProgression.Clear();

            foreach (var resource in _sortedResources.Values)
                _resources.Add(resource);

            foreach (var level in _sortedLevelsProgression.Values)
                _levelsProgression.Add(level);

            onComplete?.Invoke();
        }

        public void LoadInitialResources(GameConfigService config)
        {
            DeserializeResources(config.InitialResources);

            foreach (var level in config.Locations)
            {
                var location = new ProgressionOnLevel(level.Header, 0, false);
                _sortedLevelsProgression.Add(location.LevelName, location);
            }

            _unlockedHeros.Add(config.Heroes[0].Header);
            _saveLoadService.Save();
        }

        public void SetAmountOfResource(string resource, int amount)
        {
            var finalAmount = _sortedResources[resource].Amount + amount;

            if (finalAmount < 0)
                finalAmount = 0;

            _sortedResources[resource].Amount = finalAmount;

            _ticksPlayed++;

            _saveLoadService.Save();
        }

        public int GetAmountOfResource(string resource) => _sortedResources[resource].Amount;

        public void SetQuestCompleted(int quest)
        {
            _ticksPlayed++;

            _completedQuestIndex.Add(quest);
            _saveLoadService.Save();
        }

        public bool GetQuestCompleted(int quest) => _completedQuestIndex.Contains(quest);

        public void SetHeroUnlocked(string hero)
        {
            _ticksPlayed++;

            _unlockedHeros.Add(hero);
            _saveLoadService.Save();
        }

        public bool GetHeroUnlocked(string hero) => _unlockedHeros.Contains(hero);

        public void SetEnemyDiscovered(string enemy)
        {
            if (_discoveredEnemies.Contains(enemy))
                return;

            _ticksPlayed++;

            _discoveredEnemies.Add(enemy);
            _saveLoadService.Save();
        }

        public bool GetEnemyDiscovered(string enemy) => _discoveredEnemies.Contains(enemy);

        public int GetAmountOfProgression(string concept)
        {
            return concept switch
            {
                "Hunt" => _totalMonstersKilled,
                "Adventure" => GetHigherLevelReached(),
                "Rooms" => GetRoomsCompleted(),
                "Complete_Village" => GetLocationCompleted("Village") ? 1 : 0,
                "Complete_Sewers" => GetLocationCompleted("Sewers") ? 1 : 0,
                "Complete_Dungeons" => GetLocationCompleted("Dungeons") ? 1 : 0,
                _ => 0
            };
        }

        private int GetHigherLevelReached() => _sortedLevelsProgression.Values.Select(level => level.MaxLevel).Max();
        public void SetRoomCompleted() => _totalRoomsCleared++;
        public int GetRoomsCompleted() => _totalRoomsCleared;
        public bool GetLocationCompleted(string location) => _sortedLevelsProgression[location].Completed;
        public void SetEnemyKilled() => _totalMonstersKilled++;

        public void SetLocationProgress(string location, int floor)
        {
            _ticksPlayed++;

            var level = _sortedLevelsProgression[location];

            if (level.MaxLevel < floor)
                level.MaxLevel = floor;

            _saveLoadService.Save();
        }

        public void SetLocationCompleted(string location)
        {
            _ticksPlayed++;

            _sortedLevelsProgression[location].Completed = true;
            _saveLoadService.Save();
        }

        public void SetSoundOn(bool on)
        {
            _ticksPlayed++;

            _soundOn = on;
            _saveLoadService.Save();
        }

        public bool GetSoundOff() => _soundOn;

        private void DeserializeResources(List<ResourceElement> resources)
        {
            foreach (var resource in resources)
                _sortedResources.Add(resource.Key, resource);
        }
    }
}