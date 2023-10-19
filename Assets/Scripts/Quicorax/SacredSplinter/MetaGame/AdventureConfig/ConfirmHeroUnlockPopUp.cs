using System;
using Quicorax.SacredSplinter.MetaGame.Shop;
using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Quicorax.SacredSplinter.MetaGame.AdventureConfig
{
    public class ConfirmHeroUnlockPopUp : BasePopUp
    {
        [SerializeField] private PopUpLauncher _notEnoughResourcesPopUp;
        [SerializeField] private Button _tryUnlockButton;

        [SerializeField] private TMP_Text _heroClass, _availableHeroLicenses;

        [Inject] private IGameProgressionService _progression;
        [Inject] private IPopUpSpawnerService _popUps;
        
        private Action _onConfirm;

        private int _availableLicenses;

        public void Initialize(string product, Action onConfirm)
        {
            _onConfirm = onConfirm;
            _heroClass.text = product;

            _tryUnlockButton.onClick.AddListener(TryUnlock);
            
            _availableLicenses = _progression.GetAmountOfResource("Hero License");

            _availableHeroLicenses.text = _availableLicenses.ToString();
        }

        private void TryUnlock()
        {
            if (_availableLicenses > 0)
            {
                _onConfirm?.Invoke();
            }
            else
            {
                _popUps.SpawnPopUp<NotEnoughResources>(_notEnoughResourcesPopUp).Initialize();
            }

            CloseSelf();
        }
    }
}