using Quicorax.SacredSplinter.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Quicorax.SacredSplinter.MetaGame.UI
{
    public class GameCanvasController : MonoBehaviour
    {
        [SerializeField] private Image _background, _hero;
        [SerializeField] private TMP_Text _header, _floorNumber, _health;
        [SerializeField] private Slider _healthSlider;

        private AdventureProgressionService _adventureProgress;
        private AdventureConfigurationService _adventureConfig;
        private ElementImagesService _elementImages;

        private void Start()
        {
            _adventureProgress = ServiceLocator.GetService<AdventureProgressionService>();
            _adventureConfig = ServiceLocator.GetService<AdventureConfigurationService>();
            _elementImages = ServiceLocator.GetService<ElementImagesService>();

            _adventureProgress.StartAdventure(_adventureConfig.GetHeroData(), SetHealthData);
            SetLevelVisualData();

            SetMaxHealthData();
        }

        private void SetLevelVisualData()
        {
            var location = _adventureConfig.GetLocation();

            _header.text = location;

            _background.sprite = _elementImages.GetViewImage(location);
            _hero.sprite = _elementImages.GetViewImage(_adventureConfig.GetHeroData().Name);
        }

        private void SetMaxHealthData()
        {
            _healthSlider.maxValue = _adventureProgress.GetMaxHealth();
            _healthSlider.value = _adventureProgress.GetMaxHealth();
        }

        private void SetHealthData()
        {
            var currentHealth = _adventureProgress.GetCurrentHealth();

            _healthSlider.value =currentHealth;
            _health.text = $"{currentHealth} / {_adventureProgress.GetMaxHealth()}";
        }

        public void SetFloorNumber(int number) => _floorNumber.text = number.ToString();
    }
}