using System;
using System.Collections.Generic;
using Quicorax.SacredSplinter.GamePlay.Interactions.Combat;
using Quicorax.SacredSplinter.Models;
using UnityEngine;
using UnityEngine.Serialization;

namespace Quicorax.SacredSplinter.Services
{
    [Serializable]
    public class GameProgressionService : IService
    {
        private SaveLoadService _saveLoadService;

        [SerializeField] private int _ticksPlayed = 0;
        
        [SerializeField] private List<ResourceElement> _resources = new();
        
        [SerializeField] private List<string> _unlockedHeros = new();
        [SerializeField] private List<string> _discoveredEnemies = new();
        
        [SerializeField] private List<int> _completedQuestIndex = new();
        
        [SerializeField] private List<ProgressionOnLevel> _levelsProgression = new();

        [SerializeField] private int _totalMonstersKilled = 0;

        [SerializeField] private bool _soundOn = true;

        public void Initialize(SaveLoadService saveLoadService) => _saveLoadService = saveLoadService;

        public void LoadInitialResources(GameConfigService config)
        {
            _resources = config.InitialResources;
            _unlockedHeros.Add(config.Heroes[0].Header);
            
            foreach (var location in config.Locations)
                _levelsProgression.Add(new(location.Header, 0, false));
            
            _saveLoadService.Save();
        }

        public void SetAmountOfResource(string resource, int amount, bool save = true)
        {
            foreach (var resourcePack in _resources)
            {
                if (resourcePack.Key == resource)
                    resourcePack.Amount += amount;
            }

            _ticksPlayed++;
            
            if (save)
                _saveLoadService.Save();
        }

        public int GetAmountOfResource(string resource)
        {
            var amount = -1;

            foreach (var resourcePack in _resources)
            {
                if (resourcePack.Key == resource)
                {
                    amount = resourcePack.Amount;
                    break;
                }
            }

            return amount;
        }

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
                "Complete_Village" => GetLocationCompleted("Village"),
                "Complete_Sewer" => GetLocationCompleted("Sewers"),
                "Complete_Dungeon" => GetLocationCompleted("Dungeons"),
                _ => 0
            };
        }

        private int GetHigherLevelReached()
        {
            var higherLevel = 0;

            foreach (var item in _levelsProgression)
            {
                if (item.MaxLevel > higherLevel)
                    higherLevel = item.MaxLevel;
            }

            return higherLevel;
        }

        private int GetLocationCompleted(string location)
        {
            foreach (var item in _levelsProgression)
            {
                if (item.LevelName == location && item.Completed)
                    return 1;
            }

            return 0;
        }

        public void SetSoundOn(bool on)
        {
            _ticksPlayed++;

            _soundOn = on;
            _saveLoadService.Save();
        }

        public bool GetSoundOff() => _soundOn;
    }
}