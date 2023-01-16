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
        [SerializeField] private GameObject _healthPercentDisplay;

        public void SetData(string header, string amount, Sprite image, bool withImage)
        {
            _healthPercentDisplay.SetActive(!withImage);
            _resultImage.gameObject.SetActive(withImage);

            _resultImage.sprite = image;
            _resultText.text = header;
            _resultAmount.text = amount;
        }
    }
}