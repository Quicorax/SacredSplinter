using System.Collections.Generic;
using System.Threading.Tasks;
using Quicorax.SacredSplinter.GamePlay.Interactions.Events;
using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Models;
using Quicorax.SacredSplinter.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Quicorax.SacredSplinter.GamePlay.Interactions.Combat
{
    public sealed class CombatRoomPopUp : AdventureRoomPopUp
    {
        [SerializeField] private TMP_Text _enemyName, _enemyHealthDisplay;
        [SerializeField] private Image _enemy, _hero;
        [SerializeField] private Slider _enemyHealth;
        [SerializeField] private Button _escapeButton;
        [SerializeField] private Button _skipTurnButton;
        [SerializeField] private BaseAttack _baseAttack;
        [SerializeField] private Transform _heroAttacksHolder;
        [SerializeField] private PopUpLauncher _combatResultPopUp;

        private readonly List<BaseAttack> _availableAttacks = new();

        private EnemyInstance _enemyCombatData;
        private EnemyData _enemyData;
        private HeroData _heroData;

        private CombatInstance _combatInstance;

        private int _enemyMaxHealth;

        protected override void Initialize()
        {
            GetCommonServices();

            _enemyCombatData = new EnemyInstance();
            _enemyData = _enemyCombatData.GetEnemy();
            
            ExecuteCommonMethods();

            ConfigureEnemy();
            ConfigureHero();

            StartCombat();
        }


        protected override async Task SetSpritesAsync()
        {
            _enemy.sprite = await Addressables.LoadAddrssAsset<Sprite>(_enemyData.Header);
            _hero.sprite = await Addressables.LoadAddrssAsset<Sprite>(AdventureConfig.GetHeroData().Header);
        }

        protected override void SetButtonLogic()
        {
            _escapeButton.interactable = AdventureConfig.GetHeroData().CanEscapeCombats;

            if (_escapeButton.interactable)
                _escapeButton.onClick.AddListener(Complete);

            _skipTurnButton.onClick.AddListener(() => _combatInstance.OnPlayerSkipTurn());
        }

        private void StartCombat() => _combatInstance =
            new CombatInstance(_enemyCombatData, PlayerTurn, UpdateEnemyHealth, OnDamagePlayer, OnCombatEnded);

        private void ConfigureEnemy()
        {
            _enemyName.text = _enemyData.Header;

            _enemyData.CurrentHealth = _enemyData.MaxHealth + _enemyData.HealthEvo * (CurrentFloor - 1);
            _enemyData.CurrentSpeed = _enemyData.Speed + _enemyData.SpeedEvo * (CurrentFloor - 1);
            _enemyData.CurrentDamage = _enemyData.Damage + _enemyData.DamageEvo * (CurrentFloor - 1);

            _enemyMaxHealth = _enemyData.CurrentHealth;
            _enemyHealth.maxValue = _enemyMaxHealth;
            GameProgression.SetEnemyDiscovered(_enemyData.Header);
            UpdateEnemyHealth();
        }

        private void ConfigureHero()
        {
            _heroData = AdventureConfig.GetHeroData();

            foreach (var attack in _heroData.Attacks)
            {
                var attackOption = Instantiate(_baseAttack, _heroAttacksHolder);
                attackOption.Initialize(attack, OnAttackSelected);

                _availableAttacks.Add(attackOption);
            }
        }

        private void PlayerTurn()
        {
            foreach (var attack in _availableAttacks)
                attack.TryAwake();
        }

        private void OnAttackSelected(AttackData attack)
        {
            //Animation...
            _combatInstance.OnPlayerAttackSelected(attack);
        }

        private void OnDamagePlayer(int damage)
        {
            //Animation...
            AdventureProgression.UpdateRawHealth(damage, _enemyData.Header);
        }

        private void OnCombatEnded(bool enemyDead)
        {
            if (enemyDead)
            {
                PopUpSpawner.SpawnPopUp<CombatResultPopUp>(_combatResultPopUp).SetData(_enemyData.ExperienceOnKill);
            }

            Complete();
        }

        private void UpdateEnemyHealth()
        {
            _enemyHealth.value = _enemyData.CurrentHealth;
            _enemyHealthDisplay.text = $"{_enemyData.CurrentHealth} / {_enemyMaxHealth}";
        }
    }
}