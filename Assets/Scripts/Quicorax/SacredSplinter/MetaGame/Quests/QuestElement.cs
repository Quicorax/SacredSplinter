using System;
using System.Threading.Tasks;
using Quicorax.SacredSplinter.Models;
using Quicorax.SacredSplinter.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Quicorax.SacredSplinter.MetaGame.Quests
{
    public class QuestElement : MonoBehaviour
    {
        [SerializeField] private GameObject _claimeable;

        [SerializeField] private TMP_Text _header, _rewardAmount, _progressionPerCent;
        [SerializeField] private Button _button;
        [SerializeField] private Image _rewardIcon;

        private QuestData _quest;

        private bool _completed;
        private bool _claimed;

        private Action _onTransactionCompleted;

        private GameProgressionService _progression;
        private AddressablesService _addressables;

        public async Task Initialize(QuestData data, Action onTransactionCompleted, GameProgressionService gameProgression,
            AddressablesService addressables)
        {
            _progression = gameProgression;
            _addressables = addressables;
            
            _quest = data;
            _onTransactionCompleted = onTransactionCompleted;

            _header.text = data.Header;

            _rewardAmount.text = data.RewardAmount.ToString();
            _rewardIcon.sprite = await addressables.LoadAddrssAsset<Sprite>(data.Reward);

            _progressionPerCent.text = GetProgression();

            _claimed = _progression.GetQuestCompleted(_quest.Index);

            SetInteractable(!_claimed);
            SetClaim(_completed && !_claimed);
        }

        private string GetProgression()
        {
            var percent = _progression.GetAmountOfProgression(_quest.Concept) * 100 / _quest.Amount;
            var resultPercent = Mathf.Clamp(percent, 0, 100);

            _completed = resultPercent >= 100;

            return resultPercent.ToString();
        }

        public void CollectReward()
        {
            if (!_completed)
                return;

            _claimed = true;

            _progression.SetQuestCompleted(_quest.Index);
            _progression.SetAmountOfResource(_quest.Reward, _quest.RewardAmount);

            _onTransactionCompleted?.Invoke();

            SetClaim(false);
            SetInteractable(false);
        }

        private void SetClaim(bool canClaim) => _claimeable.SetActive(canClaim);
        private void SetInteractable(bool interactable) => _button.interactable = interactable;
    }
}