using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable] public class BaseModel { public List<BaseData> Entries = new(); }
[Serializable] public class QuestModel /*: BaseModel*/ { public List<QuestData> Quests = new(); }
[Serializable] public class ShopModel { public List<ProductData> Shop = new(); }
[Serializable] public class LocationModel : BaseModel { }
[Serializable] public class HeroModel : BaseModel { }
[Serializable] public class EncyclopediaModel : BaseModel { }

public class ModelsService : IService
{
    private QuestModel _questsModel;
    private ShopModel _shopModel;
    private LocationModel _locationModel;
    private HeroModel _heroModel;
    private EncyclopediaModel _encyclopediaModel;

    public void Initialize() => LoadModels();

    private void LoadModels()
    {
        //LoadModel<QuestModel>(_questsModel, "QuestsModel");
        LoadQuestsModel();
        LoadShopModel();
        LoadLocationsModel();
        LoadHeroModel();
        LoadEncyclopediaModel();
    }

    private void LoadQuestsModel() =>
        _questsModel = JsonUtility.FromJson<QuestModel>(Resources.Load<TextAsset>("QuestsModel").text);
    private void LoadShopModel() =>
        _shopModel = JsonUtility.FromJson<ShopModel>(Resources.Load<TextAsset>("ShopModel").text);
    private void LoadLocationsModel() =>
        _locationModel = JsonUtility.FromJson<LocationModel>(Resources.Load<TextAsset>("LocationsModel").text);
    private void LoadHeroModel() =>
        _heroModel = JsonUtility.FromJson<HeroModel>(Resources.Load<TextAsset>("HeroesModel").text);
    private void LoadEncyclopediaModel() =>
        _encyclopediaModel = JsonUtility.FromJson<EncyclopediaModel>(Resources.Load<TextAsset>("EncyclopediaModel").text);

    //private void LoadModel<T>(BaseModel model, string modelConcept) where T : BaseModel =>
    //    model = JsonUtility.FromJson<T>(Resources.Load<TextAsset>(modelConcept).text);

    public QuestModel GetQuestsModel() => _questsModel;
    public ShopModel GetShopModel() => _shopModel;
    public LocationModel GetLocationModel() => _locationModel;
    public HeroModel GetHeroesModel() => _heroModel;
    public EncyclopediaModel GetEncyclopediaModel() => _encyclopediaModel;
}
