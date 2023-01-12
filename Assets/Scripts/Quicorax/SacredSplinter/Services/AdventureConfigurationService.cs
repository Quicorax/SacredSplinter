using Quicorax.SacredSplinter.Models;
using UnityEngine;

namespace Quicorax.SacredSplinter.Services
{
    public class AdventureConfigurationService : IService
    {
        private HeroesData _heroesData;
        private string _location = string.Empty;

        public void SetHero(string heroName)
        {
            foreach (var heroData in ServiceLocator.GetService<ModelsService>().GetModel<HeroesDataModel>("HeroesData").HeroesData)
            {
                if (heroData.Name == heroName)
                {
                    _heroesData = heroData;
                    return;
                }
            }
        }
        public void SetLocation(string location) => _location = location;
        
        public string GetLocation() => _location;
        public HeroesData GetHeroData() => _heroesData;
        public bool ReadyToEngage() => _heroesData != null && _location != string.Empty;

        public void ResetSelection()
        {
            _heroesData = null;
            _location = string.Empty;
        }
    }
}