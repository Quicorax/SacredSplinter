using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class QuestModel
{
    public List<QuestData> Quests = new();
}
[Serializable]
public class ShopModel
{
    public List<ProductData> Shop = new();
}

public class ModelsService : IService
{
    private QuestModel _questsModel;
    private ShopModel _shopModel;

    public void Initialize() => LoadModels();

    private void LoadModels()
    {
        LoadQuestsModel();
        LoadShopModel();
    }

    private void LoadQuestsModel() =>
        _questsModel = JsonUtility.FromJson<QuestModel>(Resources.Load<TextAsset>("QuestsModel").text);
    private void LoadShopModel() =>
        _shopModel = JsonUtility.FromJson<ShopModel>(Resources.Load<TextAsset>("ShopModel").text);

    public QuestModel GetQuestsModel() => _questsModel;
    public ShopModel GetShopModel() => _shopModel;
}
