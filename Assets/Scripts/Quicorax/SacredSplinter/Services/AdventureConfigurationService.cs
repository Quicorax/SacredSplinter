using System.Collections.Generic;
using System.Linq;
using Quicorax.SacredSplinter.GamePlay.Interactions.Combat;
using Quicorax.SacredSplinter.Models;
using UnityEngine;
using Zenject;

namespace Quicorax.SacredSplinter.Services
{
    public interface IAdventureConfigurationService
    {
        void SetHero(HeroData hero);
        void SetLocation(LocationsData location);
        LocationsData GetLocation();
        HeroData GetHeroData();
        bool ReadyToEngage();
        void ResetSelection();
    }
    
    public class AdventureConfigurationService : IAdventureConfigurationService
    {
        [Inject] private IGameConfigService _gameConfig;
        
        private HeroData _heroData;
        private LocationsData _locationData;

        public void SetHero(HeroData hero)
        {
            _heroData = hero;
            _heroData.Attacks = SetRandomAttacks();
        }

        public void SetLocation(LocationsData location) => _locationData = location;

        public LocationsData GetLocation() => _locationData;
        public HeroData GetHeroData() => _heroData;
        public bool ReadyToEngage() => _heroData != null && _locationData != null;

        public void ResetSelection()
        {
            _heroData = null;
            _locationData = null;
        }

        private List<AttackData> SetRandomAttacks()
        {
            var tempAttacksOfKind = new List<AttackData>();

            foreach (var attack in _gameConfig.Attacks)
            {
                if (attack.AttackType == _heroData.AttackType)
                    tempAttacksOfKind.Add(attack);
            }

            while (tempAttacksOfKind.Count() > _heroData.AttackAmount)
            {
                tempAttacksOfKind.RemoveAt(Random.Range(0, tempAttacksOfKind.Count));
            }

            return tempAttacksOfKind;
        }
    }
}