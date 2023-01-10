using System;
using Quicorax.SacredSplinter.MetaGame.Shop;
using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Services;
using TMPro;
using UnityEngine;

namespace Quicorax.SacredSplinter.MetaGame.AdventureConfig
{
    public class ConfirmHeroUnlockPopUp : BasePopUp
    {
        [SerializeField] private PopUpLauncher _notEnoughtResourcesPopUp;

        [SerializeField] private TMP_Text _heroClass, _availableHeroLicenses;

        private Action _onConfirm;

        private int _availableLicenses;

        public void Initialize(string product, Action onConfirm)
        {
            _onConfirm = onConfirm;
            _heroClass.text = product;

            _availableLicenses = ServiceLocator.GetService<GameProgressionService>()
                .CheckAmountOfResource("Hero License");

            _availableHeroLicenses.text = _availableLicenses.ToString();
        }

        public void TryUnlock()
        {
            if (_availableLicenses > 0)
            {
                _onConfirm?.Invoke();
                CloseSelf();
            }
            else
            {
                ServiceLocator.GetService<PopUpSpawnerService>()
                    .SpawnPopUp<NotEnoughResources>(_notEnoughtResourcesPopUp).Initialize();

                CloseSelf();
            }
        }
    }
}