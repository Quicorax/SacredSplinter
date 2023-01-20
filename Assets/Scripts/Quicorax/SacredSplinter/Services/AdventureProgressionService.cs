using System;
using Quicorax.SacredSplinter.Models;
using Quicorax.SacredSplinter.Services.EventBus;
using UnityEngine;

namespace Quicorax.SacredSplinter.Services
{
    public class AdventureProgressionService : IService
    {
        private HeroesData _selectedHero;
        private LocationsData _selectedLocation;

        private Action _onHealthUpdate;
        private StringEventBus _onPlayerDeath;
        private GameProgressionService _gameProgression;

        private int _currentHealth;
        private int _currentFloor;
        private int _currentFloorRooms;
        private int _initialGoldCoins, _initialBlueCrystals;


        public void Initialize(GameProgressionService gameProgression, StringEventBus onPlayerDeath)
        {
            _gameProgression = gameProgression;
            _onPlayerDeath = onPlayerDeath;
        }

        public void StartAdventure(LocationsData location, HeroesData selectedHero, Action onHealthUpdate)
        {
            ResetAdventure();
            
            _selectedLocation = location;
            _selectedHero = selectedHero;
            _onHealthUpdate = onHealthUpdate;

            _currentHealth = _selectedHero.MaxHealth;

            SetInitialResources();

            AddFloor();
        }

        private void ResetAdventure()
        {
            ResetRoomCount();
            ResetFloorCount();
        }
        public int GetMaxHealth() => _selectedHero.MaxHealth;
        public int GetCurrentHealth() => _currentHealth;
        public int GetCurrentFloor() => _currentFloor;

        public void AddFloor()
        {
            _currentFloor++;
            _gameProgression.SetLocationProgress(_selectedLocation.Header, _currentFloor);
        }

        public void SetLocationCompleted() => _gameProgression.SetLocationCompleted(_selectedLocation.Header);

        public void AddRoom() => _currentFloorRooms++;
        public void ResetRoomCount() => _currentFloorRooms = 0;

        public bool IsFloorBoosTime() => _currentFloorRooms >= _selectedLocation.RoomsPerFloor;
        public bool IsLocationBoosTime() => _currentFloor >= _selectedLocation.FloorsPerLocation;

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
        private void ResetFloorCount() => _currentFloor = 0;

        private void PlayerDead(string deathReason) => _onPlayerDeath.NotifyEvent(deathReason);
    }
}