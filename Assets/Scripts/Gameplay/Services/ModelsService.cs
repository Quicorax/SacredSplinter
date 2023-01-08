using System;
using System.Collections.Generic;
using UnityEngine;


#region Cached Remote Models
public interface IModel { }
public class QuestModel : IModel { public List<QuestData> Quests = new(); }
public class ShopModel : IModel { public List<ProductData> Shop = new(); }
public class EventsModel : IModel { public List<EventData> Events = new(); }

public class BaseModel : IModel { public List<BaseData> Entries = new(); }
public class LocationModel : BaseModel { }
public class HeroModel : BaseModel { }
public class EncyclopediaModel : BaseModel { }
#endregion

public class ModelsService : IService
{
    private Dictionary<Type, IModel> _models = new();

    public void Initialize() => LoadModels();

    public T GetModel<T>() => (T)_models[typeof(T)];

    private void LoadModels()
    {
        _models.Add(typeof(QuestModel), DeSerializeModel<QuestModel>("QuestsModel"));
        _models.Add(typeof(ShopModel), DeSerializeModel<ShopModel>("ShopModel"));
        _models.Add(typeof(LocationModel), DeSerializeModel<LocationModel>("LocationsModel"));
        _models.Add(typeof(HeroModel), DeSerializeModel<HeroModel>("HeroesModel"));
        _models.Add(typeof(EncyclopediaModel), DeSerializeModel<EncyclopediaModel>("EncyclopediaModel"));
        _models.Add(typeof(EventsModel), DeSerializeModel<EventsModel>("EventsModel"));
        //Add new Model here
    }

    private T DeSerializeModel<T>(string modelConcept) where T : class =>
        JsonUtility.FromJson<T>(Resources.Load<TextAsset>(modelConcept).text);
}