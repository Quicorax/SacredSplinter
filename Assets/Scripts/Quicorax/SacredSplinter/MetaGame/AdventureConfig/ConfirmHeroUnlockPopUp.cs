using System;
using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Services;
using TMPro;
using UnityEngine;

namespace Quicorax.SacredSplinter.MetaGame.AdventureConfig
{
    public class ConfirmHeroUnlockPopUp : BasePopUp
    {
        [SerializeField] private PopUpLauncher _notEnoughResourcesPopUp;

        [SerializeField] private TMP_Text _heroClass, _availableHeroLicenses;

        private Action _onConfirm;

        private int _availableLicenses;

        public void Initialize(string product, Action onConfirm)
        {
            _onConfirm = onConfirm;
            _heroClass.text = product;

            _availableLicenses = ServiceLocator.GetService<GameProgressionService>()
                .GetAmountOfResource("Hero License");

            _availableHeroLicenses.text = _availableLicenses.ToString();
        }

        public void TryUnlock()
        {
            _onConfirm?.Invoke();
            CloseSelf();

            //if (_availableLicenses > 0) //TODO: now hero is free!
            //{
            //    _onConfirm?.Invoke();
            //}
            //else
            //{
            //   ServiceLocator.GetService<PopUpSpawnerService>()
            //       .SpawnPopUp<NotEnoughResources>(_notEnoughResourcesPopUp).Initialize();
            //}
            //    CloseSelf();
        }
    }
}