
public class QuestsPopUp : VerticalSelectablePopUp
{
    internal override void SpawnElements()
    {
        QuestModel quests = ServiceLocator.GetService<ModelsService>().GetQuestsModel();

        foreach (QuestData quest in quests.Quests)
            InstanceElement<Quest>(View).Initialize(quest, UpdateUI);
    }
}
