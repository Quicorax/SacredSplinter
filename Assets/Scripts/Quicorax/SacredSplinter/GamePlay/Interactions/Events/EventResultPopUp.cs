using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Quicorax.SacredSplinter.GamePlay.Interactions.Events
{
    public sealed class EventResultPopUp : BasePopUp
    {
        [SerializeField] private TMP_Text _resultText, _resultAmount;
        [SerializeField] private Image _resultImage;
        
        public void SetData(string header, string amount, Sprite image)
        {
            _resultText.text = header;
            _resultAmount.text = amount;
            _resultImage.sprite = image;
        }
    }
}