using Quicorax.SacredSplinter.Models;
using Quicorax.SacredSplinter.Services;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
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
        private ElementImagesService _elementImages;

        private EnemiesData _enemiesData;

        private int _enemyCurrentHealth;

        public void Start()
        {
            GetServices();

            _enemiesData = SetEnemy();

            _enemyName.text = _enemiesData.Header;
            _enemy.sprite = _elementImages.GetViewImage(_enemiesData.Header);
            _hero.sprite = _elementImages.GetViewImage(_adventureConfig.GetHeroData().Header);

            _enemyCurrentHealth = _enemiesData.MaxHealth;
            _enemyHealth.maxValue = _enemiesData.MaxHealth;

            _gameProgression.SetEnemyDiscovered(_enemiesData);
            
            UpdateEnemyHealth();
            SetButtonLogic();
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

        private void GetServices()
        {
            _adventureConfig = ServiceLocator.GetService<AdventureConfigurationService>();
            _gameProgression = ServiceLocator.GetService<GameProgressionService>();
            _elementImages = ServiceLocator.GetService<ElementImagesService>();
        }

        public void OnIgnore() => Complete();

        private void UpdateEnemyHealth()
        {
            _enemyHealth.value = _enemyCurrentHealth;
            _enemyHealthDisplay.text = $"{_enemyCurrentHealth} / {_enemiesData.MaxHealth}";
        }
    }
}