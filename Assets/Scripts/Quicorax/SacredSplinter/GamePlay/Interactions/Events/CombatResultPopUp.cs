using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Quicorax.SacredSplinter.GamePlay.Interactions.Events
{
    public class CombatResultPopUp : BasePopUp
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private TMP_Text _experienceAmount, _blueCrystalsAmount, _goldCoinsAmount;

        public void SetData(EnemyData enemyData)
        {
            _experienceAmount.text = enemyData.ExperienceOnKill.ToString();
            _blueCrystalsAmount.text = enemyData.TempBlueCrystalReward.ToString();
            _goldCoinsAmount.text = enemyData.TempGoldCoinReward.ToString();
            
            _closeButton.onClick.AddListener(CloseSelf);
        }
    }
}