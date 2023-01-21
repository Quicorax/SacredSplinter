using System.Collections.Generic;
using System.Linq;
using Quicorax.SacredSplinter.GamePlay.Interactions.Combat;
using Quicorax.SacredSplinter.Models;
using UnityEngine;

namespace Quicorax.SacredSplinter.Services
{
    public class AdventureConfigurationService : IService
    {
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
            var attacks = ServiceLocator.GetService<GameConfigService>().Attacks;
            List<AttackData> tempAttacksOfKind = new();

            foreach (var attack in attacks)
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