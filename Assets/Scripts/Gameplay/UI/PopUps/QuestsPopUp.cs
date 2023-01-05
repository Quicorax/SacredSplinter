using UnityEngine;
public class QuestsPopUp : BasePopUp
{
    [SerializeField]
    private QuestModel _questModel;

    [SerializeField]
    private Quest _questView;

    [SerializeField]
    private Transform _questHolder;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        SpawnQuests();
    }

    private void SpawnQuests()
    {
        foreach (QuestData quest in _questModel.Quests)
            Instantiate(_questView, _questHolder).Initialize(quest);
    }
}
