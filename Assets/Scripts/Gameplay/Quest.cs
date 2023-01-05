using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class QuestData
{
    public int QuestIndex;

    public string QuestHeader;
    public int AmountToComplete;
    public string ConceptToComplete;
    public Reward Reward;
}

[Serializable]
public class Reward
{
    public int Amount;

    public Item Item;
}

public class Quest : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _header, _rewardAmount, _progressionPerCent;
    [SerializeField]
    private Button _button;
    [SerializeField]
    private Image _rewardIcon;

    private QuestData _quest;

    private bool _completed;

    private GameProgressionService _progression;


    public void Initialize(QuestData data)
    {
        _progression = ServiceLocator.GetService<GameProgressionService>();

        _quest = data;

        _header.text = data.QuestHeader;
        _rewardAmount.text = data.Reward.Amount.ToString();
        _rewardIcon.sprite = data.Reward.Item.Image;
        _progressionPerCent.text = GetProgression();

        SetInteractable(!_progression.GetQuestCompleted(_quest.QuestIndex));
    }

    private string GetProgression()
    {
        int percent = _progression.GetAmountOfPregression(_quest.ConceptToComplete) * 100 / _quest.AmountToComplete;

        int resultPercent = Mathf.Clamp(percent, 0, 100);

        _completed = resultPercent >= 100;

        return resultPercent.ToString();
    }

    public void CollectReward()
    {
        if (_completed)
        {
            _progression.SetQuestCompleted(_quest.QuestIndex);
            _progression.SetAmoutOfResource(_quest.Reward.Item.Name, _quest.Reward.Amount);

            SetInteractable(false);
        }
    }

    private void SetInteractable(bool interactable) => _button.interactable = interactable;
}
