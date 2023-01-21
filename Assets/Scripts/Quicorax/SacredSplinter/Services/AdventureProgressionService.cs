using System;
using Quicorax.SacredSplinter.Models;
using Quicorax.SacredSplinter.Services.EventBus;
using UnityEngine;

namespace Quicorax.SacredSplinter.Services
{
    public class AdventureProgressionService : IService
    {
        private HeroData _selectedHero;
        private LocationsData _selectedLocation;

        private Action _onHealthUpdate;
        private StringEventBus _onPlayerDeath;
        private GameProgressionService _gameProgression;

        private int _currentHeroLevel;
        private int _currentHeroExperience;

        private int _currentHeroHealth;
        private int _currentHeroSpeed;
        private int _currentHeroDamage;

        private int _currentFloor;
        private int _currentFloorRooms;
        private int _initialGoldCoins, _initialBlueCrystals;


        public void Initialize(GameProgressionService gameProgression, StringEventBus onPlayerDeath)
        {
            _gameProgression = gameProgression;
            _onPlayerDeath = onPlayerDeath;
        }

        public void StartAdventure(LocationsData location, HeroData selectedHero, Action onHealthUpdate)
        {
            ResetAdventure();

            _selectedLocation = location;
            _selectedHero = selectedHero;
            _onHealthUpdate = onHealthUpdate;

            SetInitialHeroStats();
            SetInitialResources();

            AddFloor();
        }


        public int GetMaxHealth() => _selectedHero.MaxHealth;
        public int GetCurrentHealth() => _currentHeroHealth;
        public int GetCurrentFloor() => _currentFloor;

        public int GetCurrentHeroSpeed() => _currentHeroSpeed;
        public int GetCurrentHeroDamage() => _currentHeroDamage;

        public void AddHeroExperience(int amount)
        {
            _currentHeroExperience += amount;

            if (_currentHeroExperience >= _selectedHero.ExperienceToLvl)
            {
                HeroLevelUp();
                _currentHeroExperience -= _selectedHero.ExperienceToLvl;

                if (_currentHeroExperience > 0)
                    AddHeroExperience(_currentHeroExperience);
            }
        }

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
            _currentHeroHealth = Mathf.Clamp(_currentHeroHealth + amount, 0, _selectedHero.MaxHealth);

            if (_currentHeroHealth == 0)
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
       
        private void SetInitialHeroStats()
        {
            _currentHeroHealth = _selectedHero.MaxHealth;
            _currentHeroSpeed = _selectedHero.Speed;
            _currentHeroDamage = _selectedHero.Damage;
        }
        
        private void SetInitialResources()
        {
            _initialGoldCoins = _gameProgression.GetAmountOfResource("Gold Coin");
            _initialBlueCrystals = _gameProgression.GetAmountOfResource("Blue Crystal");
        }
        
        private void HeroLevelUp()
        {
            _currentHeroLevel++;

            _currentHeroHealth = _selectedHero.MaxHealth + _selectedHero.HealthEvo * _currentHeroLevel;
            _currentHeroSpeed = _selectedHero.Speed + _selectedHero.SpeedEvo * _currentHeroLevel;
            _currentHeroDamage = _selectedHero.Damage + _selectedHero.DamageEvo * _currentHeroLevel;
        }
        
        private void ResetAdventure()
        {
            ResetRoomCount();
            ResetFloorCount();
        }

        private void ResetFloorCount() => _currentFloor = 0;

        private void PlayerDead(string deathReason) => _onPlayerDeath.NotifyEvent(deathReason);
    }
}