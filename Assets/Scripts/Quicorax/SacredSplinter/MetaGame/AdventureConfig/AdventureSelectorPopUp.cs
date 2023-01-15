using System;
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

        private PopUpSpawnerService _popUpSpawner;
        private AdventureConfigurationService _adventureConfig;
        private ElementImagesService _elementImages;

        private SelectorPack _currentSelectorPack;

        public void Initialize()
        {
            _popUpSpawner = ServiceLocator.GetService<PopUpSpawnerService>();
            _adventureConfig = ServiceLocator.GetService<AdventureConfigurationService>();
            _elementImages = ServiceLocator.GetService<ElementImagesService>();

            _adventureConfig.ResetSelection();

            TurnObjectOn(_locationSelectionPack.Image.gameObject, false);
            TurnObjectOn(_heroSelectionPack.Image.gameObject, false);

            ActivateButton(_engageOnAdventureButton, false);
        }

        public void EngageOnAdventure()
        {
            CloseSelf();
            ServiceLocator.GetService<NavigationService>().NavigateToGame();
        }

        public void OpenLocationSelector() => SpawnSelectorPopUp<LocationSelectorPopUp>(_locationSelectionPack);
        public void OpenHeroSelector() => SpawnSelectorPopUp<HeroSelectorPopUp>(_heroSelectionPack);

        private void SpawnSelectorPopUp<T>(SelectorPack pack) where T : HorizontalSelectablePopUp
        {
            _currentSelectorPack = pack;

            ActivateButton(pack.Launcher.Button, false);

            _popUpSpawner.SpawnPopUp<T>(pack.Launcher).Initialize(OnSelectionSuccess, OnSelectionFailed);
        }

        private void OnSelectionSuccess(string header)
        {
            _currentSelectorPack.Image.sprite = _elementImages.GetViewImage(header);

            TurnObjectOn(_currentSelectorPack.Image.gameObject, true);
            TurnObjectOn(_currentSelectorPack.Unselected, false);

            ActivateButton(_currentSelectorPack.Launcher.Button, true);

            if (_adventureConfig.ReadyToEngage())
                ActivateButton(_engageOnAdventureButton, true);
        }

        private void OnSelectionFailed() => ActivateButton(_currentSelectorPack.Launcher.Button, true);

        public void CancelSelection() => _adventureConfig.ResetSelection();

        private void ActivateButton(Button button, bool activate) => button.interactable = activate;
        private void TurnObjectOn(GameObject element, bool on) => element.SetActive(on);
    }
}