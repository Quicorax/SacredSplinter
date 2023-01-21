using System.Collections.Generic;
using Quicorax.SacredSplinter.GamePlay.Interactions.Combat;
using Quicorax.SacredSplinter.Models;

namespace Quicorax.SacredSplinter.Services
{
    public class GameConfigService : IService
    {
        private RemoteConfigService _dataProvider;

        public List<ResourceElement> InitialResources { get; private set; }
        public List<QuestData> Quests { get; private set; }
        public List<ProductData> Shop { get; private set; }
        public List<LocationsData> Locations { get; private set; }
        public List<HeroData> Heroes { get; private set; }
        public List<EnemyData> Enemies { get; private set; }
        public List<EventData> Events { get; private set; }
        public List<AttackData> Attacks { get; private set; }


        public void Initialize(RemoteConfigService dataProvider)
        {
            _dataProvider = dataProvider;

            InitialResources = FromJsonToList<ResourceElement>("InitialResourcesModel");
            Quests = FromJsonToList<QuestData>("QuestsModel");
            Shop = FromJsonToList<ProductData>("ShopModel");
            Locations = FromJsonToList<LocationsData>("LocationsModel");
            Heroes = FromJsonToList<HeroData>("HeroesModel");
            Enemies = FromJsonToList<EnemyData>("EnemiesModel");
            Events = FromJsonToList<EventData>("AdventureEventsModel");
            Attacks = FromJsonToList<AttackData>("AttacksModel");
        }

        private List<T> FromJsonToList<T>(string key) => _dataProvider.GetFromJSON(key, new List<T>());
    }
}