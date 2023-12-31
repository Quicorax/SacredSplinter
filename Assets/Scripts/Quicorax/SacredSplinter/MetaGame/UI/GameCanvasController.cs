using System.Threading.Tasks;
using Quicorax.SacredSplinter.GamePlay.AdventureLoop;
using Quicorax.SacredSplinter.Initialization;
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
        [SerializeField] private AdventureProgressionLoop _adventureLoop;
        [SerializeField] private Image _background, _hero;
        [SerializeField] private TMP_Text _header, _floorNumber, _health;
        [SerializeField] private PopUpLauncher _deathPopUp;
        [SerializeField] private CurtainTransition _curtain;
        [SerializeField] private TMP_Text _heroLvl;
        [SerializeField] private Image _healthFiller;
        [SerializeField] private Image _experienceFiller;

        [SerializeField] private StringEventBus _onPlayerDeath;

        private AdventureProgressionService _adventureProgress;
        private AdventureConfigurationService _adventureConfig;
        private AddressablesService _addressables;

        public void UpdateFloorNumber() => _floorNumber.text = _adventureProgress.GetCurrentFloor().ToString();

        private void Awake() => _onPlayerDeath.Event += PlayerDeath;
        private void OnDestroy() => _onPlayerDeath.Event -= PlayerDeath;

        private void Start()
        {
            _adventureProgress = ServiceLocator.GetService<AdventureProgressionService>();
            _adventureConfig = ServiceLocator.GetService<AdventureConfigurationService>();
            _addressables = ServiceLocator.GetService<AddressablesService>();

            _adventureProgress.StartAdventure(_adventureConfig.GetLocation(), _adventureConfig.GetHeroData(),
                SetHealthData, UpdateExperience);

            SetHealthData();
            SetLevelVisualData().ManageTaskException();

            _adventureLoop.Initialize();
        }

        private async Task SetLevelVisualData()
        {
            var location = _adventureConfig.GetLocation();

            _header.text = location.Header;

            _background.sprite = await _addressables.LoadAddrssAsset<Sprite>(location.Header);
            _hero.sprite = await _addressables.LoadAddrssAsset<Sprite>(_adventureConfig.GetHeroData().Header);
        }

        private void SetHealthData()
        {
            var currentMaxHealth = _adventureProgress.GetMaxHealth();
            var currentHealth = _adventureProgress.GetCurrentHealth();

            _health.text = $"{currentHealth} / {currentMaxHealth}";

            var fillAmount = (float)currentHealth / currentMaxHealth;
            _healthFiller.fillAmount = fillAmount;
        }

        private void UpdateExperience(int heroLevel, int heroExperience)
        {
            _heroLvl.text = heroLevel.ToString();

            var fillAmount = (float)heroExperience / _adventureProgress.GetHeroExperienceToLevelUp();
            _experienceFiller.fillAmount = fillAmount;
        }

        private void PlayerDeath(string deathReason) =>
            ServiceLocator.GetService<PopUpSpawnerService>().SpawnPopUp<DeathPopUp>(_deathPopUp)
                .Initialize(deathReason, _curtain);
    }
}