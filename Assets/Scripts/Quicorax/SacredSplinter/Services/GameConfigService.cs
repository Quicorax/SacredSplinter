using System.Collections.Generic;
using Quicorax.SacredSplinter.GamePlay.Interactions.Combat;
using Quicorax.SacredSplinter.Models;

namespace Quicorax.SacredSplinter.Services
{
    public class GameConfigService : IService
    {
        public List<ResourceElement> InitialResources { get; private set; }
        public List<QuestData> Quests { get; private set; }
        public List<ProductData> Shop { get; private set; }
        public List<LocationsData> Locations { get; private set; }
        public List<HeroesData> Heroes { get; private set; }
        public List<EnemiesData> Enemies { get; private set; }
        public List<EventData> Events { get; private set; }

        
        public void Initialize(RemoteConfigService dataProvider)
        {
            InitialResources = dataProvider.GetFromJSON("InitialResourcesModel", new List<ResourceElement>());
            Quests = dataProvider.GetFromJSON("QuestsModel", new List<QuestData>());
            Shop = dataProvider.GetFromJSON("ShopModel", new List<ProductData>());
            Locations = dataProvider.GetFromJSON("LocationsModel", new List<LocationsData>());
            Heroes = dataProvider.GetFromJSON("HeroesModel", new List<HeroesData>());
            Enemies = dataProvider.GetFromJSON("EnemiesModel", new List<EnemiesData>());
            Events = dataProvider.GetFromJSON("AdventureEventsModel", new List<EventData>());
        }
    }
}