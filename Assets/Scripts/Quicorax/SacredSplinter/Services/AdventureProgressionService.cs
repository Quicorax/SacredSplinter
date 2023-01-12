using System;
using Quicorax.SacredSplinter.Models;
using UnityEngine;

namespace Quicorax.SacredSplinter.Services
{
    public class AdventureProgressionService : IService
    {
        private HeroesData _selectedHero;

        private int _currentHealth;
        private Action _onHealthUpdate;

        public void StartAdventure(HeroesData selectedHero, Action onHealthUpdate)
        {
            _selectedHero = selectedHero;
            _onHealthUpdate = onHealthUpdate;

            _currentHealth = _selectedHero.MaxHealth;
        }

        public int GetMaxHealth() => _selectedHero.MaxHealth;
        public int GetCurrentHealth() => _currentHealth;

        public void UpdateHealth(int amount)
        {
            _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, _selectedHero.MaxHealth);

           if (_currentHealth == 0)
            {
                PlayerDead();
            }

            _onHealthUpdate?.Invoke();
        }

        private void PlayerDead()
        {
            Debug.Log("DEAD");
        }
    }
}