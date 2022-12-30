using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class QuestData
{
    public string QuestHeader;
    public Reward Reward;
}

[Serializable]
public class Reward
{
    public int Amount;
    public string Kind;

    public Sprite Icon;
}

public class Quest : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _header, _rewardAmound, _progressionPerCent;
    [SerializeField]
    private Image _rewardIcon;

    private Reward RewardOnClaim;

    public void Initialize(QuestData data)
    {
        RewardOnClaim = data.Reward;

        _header.text = data.QuestHeader;
        _rewardAmound.text = data.Reward.Amount.ToString();
        _rewardIcon.sprite = data.Reward.Icon;
        _progressionPerCent.text = GetProgression();
    }
    private string GetProgression()
    {
        return "?";
    }
    public void CollectReward()
    {
        Debug.Log("CLAIMED REWARD");
    }

}
