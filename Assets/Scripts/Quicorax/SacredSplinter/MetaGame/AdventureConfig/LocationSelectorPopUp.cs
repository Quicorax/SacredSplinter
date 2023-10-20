using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Models;
using Quicorax.SacredSplinter.Services;
using UnityEngine;
using Zenject;

namespace Quicorax.SacredSplinter.MetaGame.AdventureConfig
{
    public class LocationSelectorPopUp : HorizontalSelectablePopUp
    {
        [SerializeField] private GameObject _artifactCheck;
        
        [Inject] private IGameConfigService _gameConfig;
        [Inject] private IGameProgressionService _gameProgression;

        private readonly Dictionary<int, LocationsData> _locations = new();
        private LocationsData _currentLocation;

        public override void Initialize(
            IAdventureConfigurationService adventureConfiguration,
            IAddressablesService addressables,
            Action<string> onSelect = null, Action onCancel = null)
        {
            base.Initialize(adventureConfiguration, addressables,onSelect, onCancel);

            var locations = _gameConfig.Locations;

            SetListCount(locations.Count);

            for (var i = 0; i < locations.Count; i++)
            {
                _locations.Add(i, locations[i]);
            }

            ElementChanged();
        }

        protected override void ElementChanged()
        {
            _currentLocation = _locations[ActualIndex];
            PrintElementData(_currentLocation.Header, _currentLocation.Description);
            
            _artifactCheck.SetActive(_gameProgression.GetLocationCompleted(_currentLocation.Header));
        }

        protected override void SelectElement()
        {
            AdventureConfiguration.SetLocation(_currentLocation);
            _onSelect?.Invoke(_currentLocation.Header);

            CloseSelf();
        }

        protected override void OnMiddleOfFade()
        {
        }
    }
}