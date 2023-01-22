using System;
using Quicorax.SacredSplinter.GamePlay.Interactions.Combat;
using Quicorax.SacredSplinter.Models;
using Quicorax.SacredSplinter.Services;
using UnityEngine;

namespace Quicorax.SacredSplinter.GamePlay.Interactions.Combat
{
    public class CombatInstance
    {
        private EnemyData _enemy;
        private EnemyInstance _enemyCombat;
        private Action _onPlayerTurn;
        private Action _onDamageEnemy;
        private Action<int> _onDamagePlayer;
        private Action<bool> _onCombatEnded;

        private AdventureProgressionService _adventureProgression;
        
        public CombatInstance(EnemyInstance enemy, Action onPlayerPlayerTurn, Action onDamageEnemy,
            Action<int> onDamagePlayer, Action<bool> onCombatEnded)
        {
            _enemyCombat = enemy;
            _onPlayerTurn = onPlayerPlayerTurn;
            _onDamageEnemy = onDamageEnemy;
            _onDamagePlayer = onDamagePlayer;
            _onCombatEnded = onCombatEnded;

            _adventureProgression = ServiceLocator.GetService<AdventureProgressionService>();

            _enemy = _enemyCombat.GetEnemy();
            
            SetTurn(_adventureProgression.GetCurrentHeroSpeed() >= _enemy.CurrentSpeed);
        }

        public void OnPlayerAttackSelected(AttackData attack)
        {
            _enemy.CurrentHealth -= attack.Damage * (_adventureProgression.GetCurrentHeroDamage() /100);
            if (_enemy.CurrentHealth <= 0)
            {
                _enemy.CurrentHealth = 0;
                
                _onCombatEnded?.Invoke(true);
                _adventureProgression.AddHeroExperience(_enemy.ExperienceOnKill);
                return;
            }

            _onDamageEnemy?.Invoke();

            SetTurn(false);
        }

        public void OnPlayerSkipTurn() => SetTurn(false);

        private void SetTurn(bool userTurn)
        {
            if (userTurn)
            {
                _onPlayerTurn?.Invoke();
            }
            else
            {
                var attack = _enemyCombat.GetAttack();
                
                if(attack != null)
                    _onDamagePlayer?.Invoke(-attack.Damage * _enemy.CurrentDamage/100);
                
                if (_adventureProgression.GetCurrentHealth() <= 0)
                {
                    _onCombatEnded?.Invoke(false);
                    return;
                }
                
                SetTurn(true);
            }
        }
    }
}