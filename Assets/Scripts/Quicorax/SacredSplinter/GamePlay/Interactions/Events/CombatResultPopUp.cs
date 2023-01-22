using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Quicorax.SacredSplinter.GamePlay.Interactions.Events
{
    public class CombatResultPopUp : BasePopUp
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private TMP_Text _experienceAmount;

        public void SetData(int experienceAmount)
        {
            _experienceAmount.text = experienceAmount.ToString();
            _closeButton.onClick.AddListener(CloseSelf);
        }
    }
}