using System;
using Quicorax.SacredSplinter.Models;
using Quicorax.SacredSplinter.Services.EventBus;
using UnityEngine;

namespace Quicorax.SacredSplinter.Services
{
    public class AdventureProgressionService : IService
    {
        private HeroesData _selectedHero;
        private Action _onHealthUpdate;
        private StringEventBus _onPlayerDeath;
        private GameProgressionService _gameProgression;

        private int _currentHealth;
        private int _currentFloor;
        private int _initialGoldCoins, _initialBlueCrystals;

        private string _location;
        
        public void Initialize(GameProgressionService gameProgression, StringEventBus onPlayerDeath)
        {
            _gameProgression = gameProgression;
            _onPlayerDeath = onPlayerDeath;
        }

        public void StartAdventure(string location, HeroesData selectedHero, Action onHealthUpdate)
        {
            _location = location;
            _selectedHero = selectedHero;
            _onHealthUpdate = onHealthUpdate;

            _currentHealth = _selectedHero.MaxHealth;

            SetInitialResources();
            
            AddFloor();
        }

        public int GetMaxHealth() => _selectedHero.MaxHealth;
        public int GetCurrentHealth() => _currentHealth;
        public int GetCurrentFloor() => _currentFloor;

        public void AddFloor()
        {
            _currentFloor++;
            _gameProgression.SetLocationProgress(_location ,_currentFloor);
        }

        public void UpdateRawHealth(int amount, string damageReason)
        {
            _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, _selectedHero.MaxHealth);

            if (_currentHealth == 0)
            {
                PlayerDead(damageReason);
            }

            _onHealthUpdate?.Invoke();
        }

        public void UpdateProportionalHealth(int percent, string damageReason)
        {
            var damage = ((float)percent / 100) * _selectedHero.MaxHealth;

            UpdateRawHealth(Mathf.RoundToInt(damage), damageReason);
        }

        public int GetGoldCoinsBalance() =>
            _gameProgression.GetAmountOfResource("Gold Coin") - _initialGoldCoins;

        public int GetBlueCrystalsBalance() =>
            _gameProgression.GetAmountOfResource("Blue Crystal") - _initialBlueCrystals;

        private void SetInitialResources()
        {
            _initialGoldCoins = _gameProgression.GetAmountOfResource("Gold Coin");
            _initialBlueCrystals = _gameProgression.GetAmountOfResource("Blue Crystal");
        }

        private void PlayerDead(string deathReason) => _onPlayerDeath.NotifyEvent(deathReason);
    }
}