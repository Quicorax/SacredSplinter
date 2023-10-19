using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Services;
using TMPro;
using UnityEngine;
using Zenject;

namespace Quicorax.SacredSplinter.MetaGame.UI
{
    public class ResourcesPopUp : BasePopUp
    {
        [SerializeField] private TMP_Text _crystalsAmount, _coinsAmount, _heroLicense;

        [Inject] 
        private void Initialize(IGameProgressionService progression)
        {
            _coinsAmount.text = progression.GetAmountOfResource("Gold Coin").ToString();
            _crystalsAmount.text = progression.GetAmountOfResource("Blue Crystal").ToString();
            _heroLicense.text = progression.GetAmountOfResource("Hero License").ToString();
        }
    }
}