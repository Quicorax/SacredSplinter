using System.Collections.Generic;
using System.Linq;
using Quicorax.SacredSplinter.Models;
using Quicorax.SacredSplinter.Services;
using UnityEngine;

namespace Quicorax.SacredSplinter.GamePlay.Interactions.Combat
{
    public class EnemyInstance
    {
        private EnemyData _enemy;
        private Dictionary<AttackData, int> _attacksOnCooldown = new();
        private AdventureConfigurationService _adventureConfig;

        public EnemyInstance()
        {
            _adventureConfig = ServiceLocator.GetService<AdventureConfigurationService>();
            
            _enemy = SetEnemy();
            
            SetRandomAttacks();
        }

        public EnemyData GetEnemy() => _enemy;
        
        private EnemyData SetEnemy()
        {
            EnemyData enemy = null;
            var enemySelected = false;

            var dataList = ServiceLocator.GetService<GameConfigService>().Enemies;

            while (!enemySelected)
            {
                enemy = dataList[Random.Range(0, dataList.Count)];
                if (string.IsNullOrEmpty(enemy.Location) || enemy.Location.Equals(_adventureConfig.GetLocation().Header))
                {
                    enemySelected = true;
                }
            }
            return enemy;
        }
        
        private void SetRandomAttacks()
        {
            var attacks = ServiceLocator.GetService<GameConfigService>().Attacks;
            var tempAttacksOfKind = attacks.Where(attack => attack.AttackType == _enemy.AttackType).ToList();

            while (tempAttacksOfKind.Count > _enemy.AttackAmount)
            {
                tempAttacksOfKind.RemoveAt(Random.Range(0, tempAttacksOfKind.Count));
            }

            foreach (var attack in tempAttacksOfKind)
            {
                _attacksOnCooldown.Add(attack, 0);
            }
        }

        public AttackData GetAttack()
        {
            var availableAttacks = new List<AttackData>();
            var attacks = new Dictionary<AttackData, int>();
            
            foreach (var availableAttack in _attacksOnCooldown)
            {
                if (availableAttack.Value == 0)
                {
                    availableAttacks.Add(availableAttack.Key);
                }  
                attacks.Add(availableAttack.Key, Mathf.Clamp(availableAttack.Value, 0, availableAttack.Key.CooldownTurns));
            }

            _attacksOnCooldown = attacks;
            
            var selectedAttack = availableAttacks.Count > 0 ? availableAttacks[Random.Range(0, availableAttacks.Count)] : null;
            
            if(selectedAttack != null)
                _attacksOnCooldown[selectedAttack] = selectedAttack.CooldownTurns;
            
            return selectedAttack;
        }
    }
}