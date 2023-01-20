using Quicorax.SacredSplinter.Models;
using UnityEngine;

namespace Quicorax.SacredSplinter.Services
{
    public class AdventureConfigurationService : IService
    {
        private HeroesData _heroesData;
        private LocationsData _locationData;

        public void SetHero(string heroName)
        {
            foreach (var heroData in ServiceLocator.GetService<GameConfigService>().Heroes)
            {
                if (heroData.Header == heroName)
                {
                    _heroesData = heroData;
                    return;
                }
            }
        }
        public void SetLocation(LocationsData location) => _locationData = location;
        
        public LocationsData GetLocation() => _locationData;
        public HeroesData GetHeroData() => _heroesData;
        public bool ReadyToEngage() => _heroesData != null && _locationData != null;

        public void ResetSelection()
        {
            _heroesData = null;
            _locationData = null;
        }
    }
}