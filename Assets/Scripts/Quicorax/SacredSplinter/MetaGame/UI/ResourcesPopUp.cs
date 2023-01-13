using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Services;
using TMPro;
using UnityEngine;

namespace Quicorax.SacredSplinter.MetaGame.UI
{
    public class ResourcesPopUp : BasePopUp
    {
        [SerializeField] private TMP_Text _crystalsAmount, _coinsAmount, _heroLicense;

        private GameProgressionService _progression;

        private void Start()
        {
            _progression = ServiceLocator.GetService<GameProgressionService>();

            Initialize();
        }

        private void Initialize()
        {
            _coinsAmount.text = _progression.GetAmountOfResource("Gold Coin").ToString();
            _crystalsAmount.text = _progression.GetAmountOfResource("Blue Crystal").ToString();
            _heroLicense.text = _progression.GetAmountOfResource("Hero License").ToString();
        }
    }
}