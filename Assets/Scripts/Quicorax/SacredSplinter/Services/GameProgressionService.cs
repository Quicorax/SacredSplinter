using System;
using System.Collections.Generic;
using Quicorax.SacredSplinter.Models;
using UnityEngine;

namespace Quicorax.SacredSplinter.Services
{
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
            _unlockedHeros = config.UnlockedHeroes;
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
            var amount = -1;

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

        public int CheckAmountOfProgression(string concept)
        {
            return concept switch
            {
                "Hunt" => _totalMonstersKilled,
                "Adventure" => CheckHigherLevelReached(),
                "Complete_Village" => CheckLocationCompleted("Village"),
                "Complete_Sewer" => CheckLocationCompleted("Sewers"),
                "Complete_Dungeon" => CheckLocationCompleted("Dungeons"),
                _ => 0
            };
        }

        private int CheckHigherLevelReached()
        {
            var higherLevel = 0;

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
}