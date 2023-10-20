using Quicorax.SacredSplinter.Initialization;
using Quicorax.SacredSplinter.MetaGame.AdventureConfig;
using Quicorax.SacredSplinter.MetaGame.Encyclopedia;
using Quicorax.SacredSplinter.MetaGame.Quests;
using Quicorax.SacredSplinter.MetaGame.Shop;
using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Services;
using UnityEngine;
using Zenject;

namespace Quicorax.SacredSplinter.MetaGame.UI
{
    public class MenuCanvasController : MonoBehaviour
    {
        [SerializeField] private PopUpLauncher _adventureSelector, _quests, _shop, _encyclopedia;

        [SerializeField] private CurtainTransition _curtain;

        [Inject] private IPopUpSpawnerService _popUpSpawner;
        [Inject] private INavigationService _navigation;
        [Inject] private IAdventureConfigurationService _adventureConfig;
        [Inject] private IGameProgressionService _gameProgressionService;
        [Inject] private IGameConfigService _gameConfig;
        [Inject] private IAddressablesService _addressables;

        private void Start()
        {
            SetButtonsListener();
        }

        private void SetButtonsListener()
        {
            _quests.Button.onClick.AddListener(OpenQuests);
            _shop.Button.onClick.AddListener(OpenShop);
            _encyclopedia.Button.onClick.AddListener(OpenEncyclopedia);
            _adventureSelector.Button.onClick.AddListener(OpenAdventureSelector);
        }

        private void OpenQuests()
        {
            var questsPopUp = _popUpSpawner.SpawnPopUp<QuestsPopUp>(_quests);
            questsPopUp.SpawnElements(_addressables, _gameConfig, _gameProgressionService);
        }

        private void OpenShop()
        {
            var spawnPopUp = _popUpSpawner.SpawnPopUp<ShopPopUp>(_shop);
            spawnPopUp.SetDependencies(_popUpSpawner);
            spawnPopUp.SpawnElements(_addressables, _gameConfig, _gameProgressionService);
        }

        private void OpenEncyclopedia()
        {
            var encyclopediaPopUp = _popUpSpawner.SpawnPopUp<EncyclopediaPopUp>(_encyclopedia);
            encyclopediaPopUp.SetDependencies(_gameProgressionService, _gameConfig);
            encyclopediaPopUp.Initialize(_adventureConfig, _addressables);
        }

        private void OpenAdventureSelector()
        {
            var adventureSelectorPopUp = _popUpSpawner.SpawnPopUp<AdventureSelectorPopUp>(_adventureSelector);
            adventureSelectorPopUp.Initialize(EngageOnAdventure, _popUpSpawner, _adventureConfig, _addressables);
        }

        private void EngageOnAdventure() => _curtain.CurtainOn(() => _navigation.NavigateToGame());
    }
}