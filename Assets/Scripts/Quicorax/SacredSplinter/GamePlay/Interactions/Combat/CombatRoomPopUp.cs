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
        private ElementImagesService _elementImages;

        private EnemyData _enemyData;

        private int _enemyCurrentHealth;

        public void Start()
        {
            GetServices();

            _enemyData = SetEnemy();

            _enemyName.text = _enemyData.Name;
            _enemy.sprite = _elementImages.GetViewImage(_enemyData.Name);
            _hero.sprite = _elementImages.GetViewImage(_adventureConfig.GetHeroData().Name);

            _enemyCurrentHealth = _enemyData.MaxHealth;
            _enemyHealth.maxValue = _enemyData.MaxHealth;

            _gameProgression.SetEnemyDiscovered(_enemyData.Name);
            
            UpdateEnemyHealth();
            SetButtonLogic();
        }

        private EnemyData SetEnemy()
        {
            EnemyData data = null;
            var dataSelected = false;
            
            while (!dataSelected)
            {
                data =  ServiceLocator.GetService<ModelsService>().GetModel<EnemiesDataModel>("EnemiesData").GetRandomEnemy();
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
            _enemyHealthDisplay.text = $"{_enemyCurrentHealth} / {_enemyData.MaxHealth}";
        }
    }
}