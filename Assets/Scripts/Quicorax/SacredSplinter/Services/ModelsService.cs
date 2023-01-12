using System.Collections.Generic;
using Quicorax.SacredSplinter.Models;
using UnityEngine;

namespace Quicorax.SacredSplinter.Services
{
    public class ModelsService : IService
    {
        private readonly Dictionary<string, IModel> _models = new();

        public void Initialize() => LoadModels();

        public T GetModel<T>(string modelKind) => (T)_models[modelKind];

        private void LoadModels()
        {
            _models.Add("Quests", DeSerializeModel<QuestModel>("QuestsModel"));
            _models.Add("Shop", DeSerializeModel<ShopModel>("ShopModel"));
            _models.Add("Events", DeSerializeModel<EventsModel>("EventsModel"));
            _models.Add("HeroesData", DeSerializeModel<HeroesDataModel>("HeroesDataModel"));
            _models.Add("Heroes", DeSerializeModel<BaseModel>("HeroesModel"));
            _models.Add("Locations", DeSerializeModel<BaseModel>("LocationsModel"));
            _models.Add("Encyclopedia", DeSerializeModel<BaseModel>("EncyclopediaModel"));
            //Add new Model here
        }

        private T DeSerializeModel<T>(string modelConcept) where T : class, IModel =>
            JsonUtility.FromJson<T>(Resources.Load<TextAsset>(modelConcept).text);
    }
}