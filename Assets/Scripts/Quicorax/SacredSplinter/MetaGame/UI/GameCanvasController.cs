using System;
using System.Threading.Tasks;
using Quicorax.SacredSplinter.GamePlay.AdventureLoop;
using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Services;
using Quicorax.SacredSplinter.Services.EventBus;
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
        [SerializeField] private PopUpLauncher _deathPopUp;

        [SerializeField] private StringEventBus _onPlayerDeath;

        private AdventureProgressionService _adventureProgress;
        private AdventureConfigurationService _adventureConfig;
        private AddressablesService _addressables;

        private void Awake() => _onPlayerDeath.Event += PlayerDeath;
        private void OnDestroy() => _onPlayerDeath.Event -= PlayerDeath;

        private void Start()
        {
            _adventureProgress = ServiceLocator.GetService<AdventureProgressionService>();
            _adventureConfig = ServiceLocator.GetService<AdventureConfigurationService>();
            _addressables = ServiceLocator.GetService<AddressablesService>();

            _adventureProgress.StartAdventure(_adventureConfig.GetLocation(), _adventureConfig.GetHeroData(),
                SetHealthData);

            SetMaxHealthData();
            SetLevelVisualData().ManageTaskException();
        }

        private async Task SetLevelVisualData()
        {
            var location = _adventureConfig.GetLocation();

            _header.text = location;

            _background.sprite = await _addressables.LoadAddrssAsset<Sprite>(location);
            _hero.sprite = await _addressables.LoadAddrssAsset<Sprite>(_adventureConfig.GetHeroData().Header);
        }

        private void SetMaxHealthData()
        {
            _healthSlider.maxValue = _adventureProgress.GetMaxHealth();

            SetHealthData();
        }

        private void SetHealthData()
        {
            var currentHealth = _adventureProgress.GetCurrentHealth();

            _healthSlider.value = currentHealth;
            _health.text = $"{currentHealth} / {_adventureProgress.GetMaxHealth()}";
        }

        public void SetFloorNumber(int number)
        {
            _floorNumber.text = number.ToString();
            _adventureProgress.AddFloor();
        }

        private void PlayerDeath(string deathReason) =>
            ServiceLocator.GetService<PopUpSpawnerService>().SpawnPopUp<DeathPopUp>(_deathPopUp)
                .Initialize(deathReason);
    }
}