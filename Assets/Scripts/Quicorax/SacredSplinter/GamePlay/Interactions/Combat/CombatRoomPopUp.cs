using System.Threading.Tasks;
using Quicorax.SacredSplinter.Models;
using Quicorax.SacredSplinter.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Quicorax.SacredSplinter.GamePlay.Interactions.Combat
{
    public sealed class CombatRoomPopUp : BaseRoomPopUp
    {
        [SerializeField] private TMP_Text _enemyName, _enemyHealthDisplay;
        [SerializeField] private Image _enemy, _hero;
        [SerializeField] private Slider _enemyHealth;
        [SerializeField] private Button _escapeButton;

        private AdventureConfigurationService _adventureConfig;
        private GameProgressionService _gameProgression;
        private AddressablesService _addressables;

        private EnemiesData _enemiesData;

        private int _enemyCurrentHealth;

        public void Start()
        {
            _adventureConfig = ServiceLocator.GetService<AdventureConfigurationService>();
            _gameProgression = ServiceLocator.GetService<GameProgressionService>();
            _addressables = ServiceLocator.GetService<AddressablesService>();

            _enemiesData = SetEnemy();

            _enemyName.text = _enemiesData.Header;

            SetSpritesAsync().ManageTaskException();

            _enemyCurrentHealth = _enemiesData.MaxHealth;
            _enemyHealth.maxValue = _enemiesData.MaxHealth;

            _gameProgression.SetEnemyDiscovered(_enemiesData.Header);
            
            UpdateEnemyHealth();
            SetButtonLogic();
        }

        private async Task SetSpritesAsync()
        {
            _enemy.sprite = await _addressables.LoadAddrssAsset<Sprite>(_enemiesData.Header);
            _hero.sprite = await _addressables.LoadAddrssAsset<Sprite>(_adventureConfig.GetHeroData().Header);
        }
        private EnemiesData SetEnemy()
        {
            EnemiesData data = null;
            var dataSelected = false;
            
            var dataList = ServiceLocator.GetService<GameConfigService>().Enemies;
            
            while (!dataSelected)
            {
                data =  dataList[Random.Range(0, dataList.Count)];
                if (string.IsNullOrEmpty(data.Location) || data.Location.Equals(_adventureConfig.GetLocation()))
                {
                    dataSelected = true;
                }
            }
            
            return data;
        }
        private void SetButtonLogic()
        {
            _escapeButton.interactable = _adventureConfig.GetHeroData().CanEscapeCombats;

            if (_escapeButton.interactable)
                _escapeButton.onClick.AddListener(Complete);
        }

        public void OnIgnore() => Complete();

        private void UpdateEnemyHealth()
        {
            _enemyHealth.value = _enemyCurrentHealth;
            _enemyHealthDisplay.text = $"{_enemyCurrentHealth} / {_enemiesData.MaxHealth}";
        }
    }
}