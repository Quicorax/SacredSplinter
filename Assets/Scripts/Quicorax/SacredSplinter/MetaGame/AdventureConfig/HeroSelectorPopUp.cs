using System;
using System.Collections.Generic;
using Quicorax.SacredSplinter.MetaGame.UI;
using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Models;
using Quicorax.SacredSplinter.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Quicorax.SacredSplinter.MetaGame.AdventureConfig
{
    public class HeroSelectorPopUp : HorizontalSelectablePopUp
    {
        [SerializeField] private GameObject _selectable;
        [SerializeField] private PopUpLauncher _unlockHeroPopUp, _heroStats;

        [SerializeField] private TMP_Text _selectText;
        [SerializeField] private Button _stats;

        private bool _elementUnlocked;

        [Inject] private IGameProgressionService _progression;
        [Inject] private IPopUpSpawnerService _popUpSpawner;
        [Inject] private IGameConfigService _gameConfig;

        private Dictionary<int, HeroData> _heroes = new();
        private HeroData _currentHero;

        public override void Initialize(Action<string> onSelect, Action onCancel)
        {

            base.Initialize(onSelect, onCancel);

            var heroes = _gameConfig.Heroes;

            SetListCount(heroes.Count);
            _stats.onClick.AddListener(ShowHeroStats);

            for (var i = 0; i < heroes.Count; i++)
                _heroes.Add(i, heroes[i]);

            ElementChanged();
        }

        protected override void ElementChanged()
        {
            _currentHero = _heroes[ActualIndex];
            PrintElementData( _currentHero.Header, _currentHero.Description);

            _elementUnlocked = _progression.GetHeroUnlocked(_currentHero.Header);

            _selectable.SetActive(_elementUnlocked);
            _selectText.text = _elementUnlocked ? "Select" : "Unlock";
        }

        public void ShowHeroStats() => _popUpSpawner.SpawnPopUp<HeroStatsPopUp>(_heroStats).SetData(_currentHero);

        protected override void SelectElement()
        {
            if (_elementUnlocked)
            {
                _adventureConfiguration.SetHero(_currentHero);
                _onSelect?.Invoke(_currentHero.Header);
                CloseSelf();
            }
            else
            {
                _popUpSpawner.SpawnPopUp<ConfirmHeroUnlockPopUp>(_unlockHeroPopUp)
                    .Initialize(_currentHero.Header, OnHeroUnlocked);
            }
        }

        protected override void OnMiddleOfFade()
        {
        }

        private void OnHeroUnlocked()
        {
            _progression.SetAmountOfResource("Hero License", -1);
            _progression.SetHeroUnlocked(_currentHero.Header);

            ElementChanged();
            SelectElement();
        }
    }
}