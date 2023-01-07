using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class QuestModel
{
    public List<QuestData> Quests = new();
}

public class ModelsService : IService
{
    private QuestModel _questsModel;

    public void Initialize() => LoadModels();

    private void LoadModels()
    {
        LoadQuestsModel();
    }

    private void LoadQuestsModel()
    {
        _questsModel = JsonUtility.FromJson<QuestModel>(Resources.Load<TextAsset>("QuestsModel").text);
    }

    public QuestModel GetQuestsModel() => _questsModel;
}
