﻿using System.Collections.Generic;
using System.Linq;
using Quicorax.SacredSplinter.Models;
using Quicorax.SacredSplinter.Services;
using UnityEngine;

namespace Quicorax.SacredSplinter.GamePlay.Interactions.Combat
{
    public class EnemyInstance
    {
        private EnemyData _enemyData;
        private List<AvailableAttack> _attackOfMyType = new();

        private AdventureConfigurationService _adventureConfig;
        private AdventureProgressionService _adventureProgression;

        private int _currentFloor;

        public EnemyInstance(int floor)
        {
            _currentFloor = floor;

            _adventureConfig = ServiceLocator.GetService<AdventureConfigurationService>();
            _adventureProgression = ServiceLocator.GetService<AdventureProgressionService>();

            _enemyData = SetEnemy();
            SetEnemyStats();
            SetRandomAttacks();
        }

        private void SetEnemyStats()
        {
            _enemyData.CurrentHealth = _enemyData.MaxHealth + _enemyData.HealthEvo * (_currentFloor - 1);
            _enemyData.CurrentSpeed = _enemyData.Speed + _enemyData.SpeedEvo * (_currentFloor - 1);
            _enemyData.CurrentDamage = _enemyData.Damage + _enemyData.DamageEvo * (_currentFloor - 1);
        }

        public EnemyData GetEnemy() => _enemyData;

        private EnemyData SetEnemy()
        {
            EnemyData enemy = null;
            var enemySelected = false;

            var dataList = ServiceLocator.GetService<GameConfigService>().Enemies;

            while (!enemySelected)
            {
                enemy = dataList[Random.Range(0, dataList.Count)];

                if (CheckSpawnConditions(enemy))
                {
                    enemySelected = true;
                }
            }

            return enemy;
        }

        private bool CheckSpawnConditions(EnemyData enemy)
        {
            return enemy.Type.Equals(_adventureProgression.GetCombatType()) &&
                   _adventureProgression.GetCurrentFloor() >= enemy.MinFloor  &&
                   (string.IsNullOrEmpty(enemy.Location) ||
                    enemy.Location.Equals(_adventureConfig.GetLocation().Header));
        }

        private void SetRandomAttacks()
        {
            var attacks = ServiceLocator.GetService<GameConfigService>().Attacks;
            var tempAttacksOfKind = attacks.Where(attack => attack.AttackType == _enemyData.AttackType).ToList();

            while (tempAttacksOfKind.Count > _enemyData.AttackAmount)
            {
                tempAttacksOfKind.RemoveAt(Random.Range(0, tempAttacksOfKind.Count));
            }

            foreach (var attack in tempAttacksOfKind)
            {
                _attackOfMyType.Add(new(attack, 0));
            }
        }

        public AttackData GetAttack()
        {
            var availableAttacks = new List<AvailableAttack>();

            foreach (var attackOfType in _attackOfMyType)
            {
                if (attackOfType.CurrentCooldown == 0)
                {
                    availableAttacks.Add(attackOfType);
                }

                attackOfType.CurrentCooldown = attackOfType.CurrentCooldown <= 0 ? 0 : attackOfType.CurrentCooldown--;
            }

            if (availableAttacks.Count > 0)
            {
                return availableAttacks[Random.Range(0, availableAttacks.Count)].Attack;
            }
            
            return null;
        }

        private class AvailableAttack
        {
            public AttackData Attack;
            public int CurrentCooldown;

            public AvailableAttack(AttackData attack, int currentCooldown)
            {
                Attack = attack;
                CurrentCooldown = currentCooldown;
            }
        }
    }
}