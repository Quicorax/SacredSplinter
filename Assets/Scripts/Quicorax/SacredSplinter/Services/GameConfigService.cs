using System.Collections.Generic;
using Quicorax.SacredSplinter.GamePlay.Interactions.Combat;
using Quicorax.SacredSplinter.Models;

namespace Quicorax.SacredSplinter.Services
{
    public interface IGameConfigService
    {
        List<ResourceElement> InitialResources { get; set; }
        List<QuestData> Quests { get; set; }
        List<ProductData> Shop { get; set; }
        List<LocationsData> Locations { get; set; }
        List<HeroData> Heroes { get; set; }
        List<EnemyData> Enemies { get; set; }
        List<EventData> Events { get; set; }
        List<AttackData> Attacks { get; set; }
        
        void Initialize(RemoteConfigService dataProvider);
    }
    
    public class GameConfigService : IGameConfigService
    {
        private RemoteConfigService _dataProvider;

        public List<ResourceElement> InitialResources { get; set; }
        public List<QuestData> Quests { get; set; }
        public List<ProductData> Shop { get; set; }
        public List<LocationsData> Locations { get; set; }
        public List<HeroData> Heroes { get; set; }
        public List<EnemyData> Enemies { get; set; }
        public List<EventData> Events { get; set; }
        public List<AttackData> Attacks { get; set; }


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