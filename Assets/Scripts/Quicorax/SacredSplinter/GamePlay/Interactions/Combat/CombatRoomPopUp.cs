using System.Threading.Tasks;
using Quicorax.SacredSplinter.Models;
using Quicorax.SacredSplinter.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Quicorax.SacredSplinter.GamePlay.Interactions.Combat
{
    public sealed class CombatRoomPopUp : AdventureRoomPopUp
    {
        [SerializeField] private TMP_Text _enemyName, _enemyHealthDisplay;
        [SerializeField] private Image _enemy, _hero;
        [SerializeField] private Slider _enemyHealth;
        [SerializeField] private Button _escapeButton;

        private EnemiesData _enemiesData;

        private int _enemyCurrentHealth;
        private int _enemyMaxHealth;

        protected override void Initialize()
        {
            GetCommonServices();
            
            _enemiesData = SetEnemy();
            ExecuteCommonMethods();

            _enemyName.text = _enemiesData.Header;

            _enemyCurrentHealth = _enemiesData.MaxHealth + _enemiesData.HealthEvo * (CurrentFloor - 1);
            _enemyMaxHealth = _enemyCurrentHealth;
            
            _enemyHealth.maxValue = _enemyMaxHealth;

            GameProgression.SetEnemyDiscovered(_enemiesData.Header);
            
            UpdateEnemyHealth();
        }

        protected override async Task SetSpritesAsync()
        {
            _enemy.sprite = await Addressables.LoadAddrssAsset<Sprite>(_enemiesData.Header);
            _hero.sprite = await Addressables.LoadAddrssAsset<Sprite>(AdventureConfig.GetHeroData().Header);
        }
      
        protected override void SetButtonLogic()
        {
            _escapeButton.interactable = AdventureConfig.GetHeroData().CanEscapeCombats;

            if (_escapeButton.interactable)
                _escapeButton.onClick.AddListener(Complete);
        }
        
        private EnemiesData SetEnemy()
        {
            EnemiesData data = null;
            var dataSelected = false;
            
            var dataList = ServiceLocator.GetService<GameConfigService>().Enemies;
            
            while (!dataSelected)
            {
                data =  dataList[Random.Range(0, dataList.Count)];
                if (string.IsNullOrEmpty(data.Location) || data.Location.Equals(AdventureConfig.GetLocation().Header))
                {
                    dataSelected = true;
                }
            }
            
            return data;
        }

        private void UpdateEnemyHealth()
        {
            _enemyHealth.value = _enemyCurrentHealth;
            _enemyHealthDisplay.text = $"{_enemyCurrentHealth} / {_enemyMaxHealth}";
        }
    }
}