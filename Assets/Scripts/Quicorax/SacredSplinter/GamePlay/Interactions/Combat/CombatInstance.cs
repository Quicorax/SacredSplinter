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
        private HeroData _hero;
        private EnemyData _enemy;
        private EnemyInstance _enemyCombat;
        private Action<bool> _onPlayerTurn;
        private Action _onDamageEnemy;
        private Action<int> _onDamagePlayer;
        private Action<bool> _onCombatEnded;
        private CombatLog _combatLog;

        private AdventureProgressionService _adventureProgression;
        private GameProgressionService _gameProgression;

        public CombatInstance(HeroData hero, EnemyInstance enemy, Action<bool> onPlayerPlayerTurn, Action onDamageEnemy,
            Action<int> onDamagePlayer, Action<bool> onCombatEnded, CombatLog combatLog)
        {
            _hero = hero;
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
            if (Random.Range(0, 100) > _enemy.CurrentAgility)
            {
                var onWeaknessDamage = attack.AttackType == _enemy.WeaknessType ? 1.5f : 1;
                var heroDamage = (float)_adventureProgression.GetCurrentHeroDamage() / 100;
                var finalDamage = Mathf.FloorToInt(attack.Damage * heroDamage * onWeaknessDamage);

                _enemy.CurrentHealth -= finalDamage;
                _combatLog.SetCombatLog("Hero", _enemy.Header, finalDamage, attack.Header);

                if (_enemy.CurrentHealth <= 0)
                {
                    _enemy.CurrentHealth = 0;

                    _onDamageEnemy?.Invoke();

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
                else
                    _onDamageEnemy?.Invoke();
            }
            else
            {
                _combatLog.SetAttackAvoidedLog(_enemy.Header);
            }

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
                    if (Random.Range(0, 100) > _adventureProgression.GetCurrentHeroAgility())
                    {
                        var onWeaknessDamage = attack.AttackType == _hero.WeaknessType ? 1.5f : 1;
                        var enemyDamage = ((float)_enemy.CurrentDamage / 100);
                        var finalDamage = Mathf.FloorToInt(attack.Damage * enemyDamage * onWeaknessDamage);

                        _onDamagePlayer?.Invoke(-finalDamage);

                        _combatLog.SetCombatLog(_enemy.Header, "Hero", finalDamage, attack.Header);
                    }
                    else
                    {
                        _combatLog.SetAttackAvoidedLog(_hero.Header);
                    }
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