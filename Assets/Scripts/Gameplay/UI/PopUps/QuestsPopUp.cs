using UnityEngine;

public class QuestsPopUp : VerticalSelectablePopUp
{
    [SerializeField]
    private QuestModel _questModel;

    [SerializeField]
    private Transform _questHolder;

    internal override void SpawnElements()
    {
        foreach (QuestData quest in _questModel.Quests)
            InstanceElement<Quest>(View).Initialize(quest, UpdateUI);
    }
}
