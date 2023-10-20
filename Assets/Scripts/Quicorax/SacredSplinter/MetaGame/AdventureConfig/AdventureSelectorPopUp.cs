using System;
using System.Threading.Tasks;
using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Models;
using Quicorax.SacredSplinter.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Quicorax.SacredSplinter.MetaGame.AdventureConfig
{
    public class AdventureSelectorPopUp : BasePopUp
    {
        [SerializeField] private SelectorPack _heroSelectionPack, _locationSelectionPack;

        [SerializeField] private Button _engageOnAdventureButton;

        private SelectorPack _currentSelectorPack;

        private IPopUpSpawnerService _popUpSpawner;
        private IAdventureConfigurationService _adventureConfig;
        private IAddressablesService _addressables;

        private Action _onEngage;

        public void Initialize(Action onEngage,
            IPopUpSpawnerService popUpSpawnerService,
            IAdventureConfigurationService adventureConfigurationService,
            IAddressablesService addressablesService)
        {
            _onEngage = onEngage;
            _popUpSpawner = popUpSpawnerService;
            _adventureConfig = adventureConfigurationService;
            _addressables = addressablesService;

            _locationSelectionPack.Launcher.Button.onClick.AddListener(OpenLocationSelector);
            _heroSelectionPack.Launcher.Button.onClick.AddListener(OpenHeroSelector);
            _engageOnAdventureButton.onClick.AddListener(EngageOnAdventure);

            _adventureConfig.ResetSelection();

            TurnObjectOn(_locationSelectionPack.Image.gameObject, false);
            TurnObjectOn(_heroSelectionPack.Image.gameObject, false);

            ActivateButton(_engageOnAdventureButton, false);
        }

        private void EngageOnAdventure()
        {
            CloseSelf();
            _onEngage?.Invoke();
        }

        private void OpenLocationSelector() => SpawnSelectorPopUp<LocationSelectorPopUp>(_locationSelectionPack);
        private void OpenHeroSelector() => SpawnSelectorPopUp<HeroSelectorPopUp>(_heroSelectionPack);

        private void SpawnSelectorPopUp<T>(SelectorPack pack) where T : HorizontalSelectablePopUp
        {
            _currentSelectorPack = pack;

            ActivateButton(pack.Launcher.Button, false);

            _popUpSpawner.SpawnPopUp<T>(pack.Launcher).Initialize(_adventureConfig, _addressables, OnSelectionSuccess, OnSelectionFailed);
        }

        private void OnSelectionSuccess(string header)
        {
            SetImage(header).ManageTaskException();

            TurnObjectOn(_currentSelectorPack.Image.gameObject, true);
            TurnObjectOn(_currentSelectorPack.Unselected, false);

            ActivateButton(_currentSelectorPack.Launcher.Button, true);

            if (_adventureConfig.ReadyToEngage())
            {
                ActivateButton(_engageOnAdventureButton, true);
            }
        }

        private async Task SetImage(string header) =>
            _currentSelectorPack.Image.sprite = await _addressables.LoadAddrssAsset<Sprite>(header);

        private void OnSelectionFailed() => ActivateButton(_currentSelectorPack.Launcher.Button, true);

        public void CancelSelection() => _adventureConfig.ResetSelection();

        private void ActivateButton(Button button, bool activate) => button.interactable = activate;
        private void TurnObjectOn(GameObject element, bool on) => element.SetActive(on);
    }
}