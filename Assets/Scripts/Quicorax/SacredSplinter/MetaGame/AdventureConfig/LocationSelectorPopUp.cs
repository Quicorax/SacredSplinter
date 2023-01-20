using System;
using System.Collections.Generic;
using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Models;
using Quicorax.SacredSplinter.Services;
using UnityEngine;

namespace Quicorax.SacredSplinter.MetaGame.AdventureConfig
{
    public class LocationSelectorPopUp : HorizontalSelectablePopUp
    {
        [SerializeField] private GameObject _artifactCheck;

        private Dictionary<int, LocationsData> _locations = new();
        private LocationsData _currentLocation;

        public override void Initialize(Action<string> onSelect, Action onCancel)
        {
            base.Initialize(onSelect, onCancel);

            var locations = ServiceLocator.GetService<GameConfigService>().Locations;

            SetListCount(locations.Count);

            for (var i = 0; i < locations.Count; i++)
                _locations.Add(i, locations[i]);

            ElementChanged();
        }

        protected override void ElementChanged()
        {
            _currentLocation = _locations[ActualIndex];
            PrintElementData( _currentLocation.Header, _currentLocation.Description);
            
            _artifactCheck.SetActive(ServiceLocator.GetService<GameProgressionService>()
                .GetLocationCompleted(_currentLocation.Header));
        }

        protected override void SelectElement()
        {
            _adventureConfiguration.SetLocation(_currentLocation);
            _onSelect?.Invoke(_currentLocation.Header);

            CloseSelf();
        }

        protected override void OnMiddleOfFade()
        {
        }
    }
}