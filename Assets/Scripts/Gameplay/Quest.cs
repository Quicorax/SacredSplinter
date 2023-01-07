using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class QuestData
{
    public int Index;
    public string Concept;

    public string Header;
    public int Amount;

    public string Reward;
    public int RewardAmount;
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

        _header.text = data.Header;
        _rewardAmount.text = data.Reward;
        //_rewardIcon.sprite = data.Reward.Item.Image; //TODO: link reward name with image
        _progressionPerCent.text = GetProgression();

        _claimed = _progression.CheckQuestCompleted(_quest.Index);

        SetInteractable(!_claimed);
        SetClaimeable(_completed && !_claimed);

    }

    private string GetProgression()
    {
        int percent = _progression.CheckAmountOfPregression(_quest.Concept) * 100 / _quest.Amount;
        int resultPercent = Mathf.Clamp(percent, 0, 100);

        _completed = resultPercent >= 100;

        return resultPercent.ToString();
    }

    public void CollectReward()
    {
        if (_completed)
        {
            _claimed = true;

            _progression.SetQuestCompleted(_quest.Index);
            _progression.SetAmountOfResource(_quest.Reward, _quest.RewardAmount);

            _onTransactionCompleted?.Invoke();

            SetClaimeable(false);
            SetInteractable(false);
        }
    }

    private void SetClaimeable(bool claimeable) => _claimeable.SetActive(claimeable);
    private void SetInteractable(bool interactable)  => _button.interactable = interactable;
}
