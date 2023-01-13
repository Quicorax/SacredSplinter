﻿using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Services;
using TMPro;
using UnityEngine;

namespace Quicorax.SacredSplinter.MetaGame.AdventureConfig
{
    public class HeroSelectorPopUp : HorizontalSelectablePopUp
    {
        [SerializeField] private GameObject _selectable;
        [SerializeField] private PopUpLauncher _unlockHeroPopUp, _heroStats;

        [SerializeField] private TMP_Text _selectText;

        private bool _elementUnlocked;

        private GameProgressionService _progression;
        private PopUpSpawnerService _popUpSpawner;

        private void Start()
        {
            _progression = ServiceLocator.GetService<GameProgressionService>();
            _popUpSpawner = ServiceLocator.GetService<PopUpSpawnerService>();

            ElementChanged();
        }


        public void ElementChanged()
        {
            _elementUnlocked = _progression.GetHeroUnlocked(CurrentElement.Header);

            _selectable.SetActive(_elementUnlocked);
            _selectText.text = _elementUnlocked ? "Select" : "Unlock";
        }

        public void ShowHeroStats()
        {
            _popUpSpawner.SpawnPopUp(_heroStats); 
        }

        public void SelectElement()
        {
            if (_elementUnlocked)
            {
                OnSelect?.Invoke(Model.Entries[ActualIndex]);
                CloseSelf();
            }
            else
            {
                _popUpSpawner.SpawnPopUp<ConfirmHeroUnlockPopUp>(_unlockHeroPopUp)
                    .Initialize(CurrentElement.Header, OnHeroUnlocked);
            }
        }

        private void OnHeroUnlocked()
        {
            _progression.SetAmountOfResource("Hero License", -1);
            _progression.SetHeroUnlocked(CurrentElement.Header);

            ElementChanged();
            SelectElement();
        }
    }
}