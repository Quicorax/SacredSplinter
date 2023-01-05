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
    private GameObject _claimeable;

    [SerializeField]
    private TMP_Text _header, _rewardAmount, _progressionPerCent;
    [SerializeField]
    private Button _button;
    [SerializeField]
    private Image _rewardIcon;

    private QuestData _quest;

    private bool _completed;
    private bool _claimed;

    private Action _onTransactionCompleted;

    private GameProgressionService _progression;

    public void Initialize(QuestData data, Action onTransactionCompleted)
    {
        _progression = ServiceLocator.GetService<GameProgressionService>();

        _quest = data;
        _onTransactionCompleted = onTransactionCompleted;

        _header.text = data.QuestHeader;
        _rewardAmount.text = data.Reward.Amount.ToString();
        _rewardIcon.sprite = data.Reward.Item.Image;
        _progressionPerCent.text = GetProgression();

        _claimed = _progression.GetQuestCompleted(_quest.QuestIndex);

        SetInteractable(!_claimed);
        SetClaimeable(_completed && !_claimed);

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
            _claimed = true;

            _progression.SetQuestCompleted(_quest.QuestIndex);
            _progression.SetAmoutOfResource(_quest.Reward.Item.Name, _quest.Reward.Amount);

            _onTransactionCompleted?.Invoke();

            SetClaimeable(false);
            SetInteractable(false);
        }
    }

    private void SetClaimeable(bool claimeable) => _claimeable.SetActive(claimeable);
    private void SetInteractable(bool interactable)  => _button.interactable = interactable;
}
