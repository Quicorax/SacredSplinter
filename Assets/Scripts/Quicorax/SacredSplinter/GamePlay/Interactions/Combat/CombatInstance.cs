using System;
using System.Threading.Tasks;
using Quicorax.SacredSplinter.Models;
using Quicorax.SacredSplinter.Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Quicorax.SacredSplinter.GamePlay.Interactions.Combat
{
    public class CombatInstance
    {
        private EnemyData _enemy;
        private EnemyInstance _enemyCombat;
        private Action<bool> _onPlayerTurn;
        private Action _onDamageEnemy;
        private Action<int> _onDamagePlayer;
        private Action<bool> _onCombatEnded;
        private CombatLog _combatLog;

        private AdventureProgressionService _adventureProgression;
        private GameProgressionService _gameProgression;

        public CombatInstance(EnemyInstance enemy, Action<bool> onPlayerPlayerTurn, Action onDamageEnemy,
            Action<int> onDamagePlayer, Action<bool> onCombatEnded, CombatLog combatLog)
        {
            _enemyCombat = enemy;
            _onPlayerTurn = onPlayerPlayerTurn;
            _onDamageEnemy = onDamageEnemy;
            _onDamagePlayer = onDamagePlayer;
            _onCombatEnded = onCombatEnded;
            _combatLog = combatLog;

            _adventureProgression = ServiceLocator.GetService<AdventureProgressionService>();
            _gameProgression = ServiceLocator.GetService<GameProgressionService>();

            _enemy = _enemyCombat.GetEnemy();

            SetTurn(_adventureProgression.GetCurrentHeroSpeed() >= _enemy.CurrentSpeed).ManageTaskException();
        }

        public void OnPlayerAttackSelected(AttackData attack)
        {
            var finalDamage = attack.Damage * (_adventureProgression.GetCurrentHeroDamage() / 100);
            _enemy.CurrentHealth -= finalDamage;
            _combatLog.SetCombatLog("Hero", _enemy.Header, finalDamage, attack.Header);

            if (_enemy.CurrentHealth <= 0)
            {
                _enemy.CurrentHealth = 0;

                _adventureProgression.AddHeroExperience(_enemy.ExperienceOnKill);

                var blueCrystalReward = RandomizeResourceAmount(_enemy.BlueCrystalsOnKill);
                var goldCoinReward = RandomizeResourceAmount(_enemy.GoldCoinsOnKill);

                _enemy.TempBlueCrystalReward = blueCrystalReward;
                _enemy.TempGoldCoinReward = goldCoinReward;

                _gameProgression.SetAmountOfResource("Blue Crystal", blueCrystalReward);
                _gameProgression.SetAmountOfResource("Gold Coin", goldCoinReward);

                _onCombatEnded?.Invoke(true);
                return;
            }

            _onDamageEnemy?.Invoke();

            SetTurn(false).ManageTaskException();
        }

        public void OnPlayerSkipTurn()
        {
            _combatLog.SkipTurnLog("Hero");
            SetTurn(false).ManageTaskException();
        }


        private int RandomizeResourceAmount(int initialAmount) =>
            Mathf.FloorToInt(initialAmount * Random.Range(0.5f, 1.5f));

        private async Task SetTurn(bool userTurn)
        {
            _onPlayerTurn?.Invoke(userTurn);

            if (!userTurn)
            {
                await Task.Delay(1000);

                var attack = _enemyCombat.GetAttack();

                if (attack != null)
                {
                    var finalDamage = attack.Damage * _enemy.CurrentDamage / 100;
                    _onDamagePlayer?.Invoke(-finalDamage);

                    _combatLog.SetCombatLog(_enemy.Header, "Hero", finalDamage, attack.Header);
                }
                else
                    _combatLog.SkipTurnLog(_enemy.Header);

                if (_adventureProgression.GetCurrentHealth() <= 0)
                {
                    _onCombatEnded?.Invoke(false);
                    return;
                }
                
                await Task.Delay(1000);

                SetTurn(true).ManageTaskException();
            }
        }
    }
}