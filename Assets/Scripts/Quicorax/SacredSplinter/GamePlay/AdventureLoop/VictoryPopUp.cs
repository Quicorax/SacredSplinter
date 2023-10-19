using System.Threading.Tasks;
using Quicorax.SacredSplinter.Initialization;
using Quicorax.SacredSplinter.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Quicorax.SacredSplinter.GamePlay.AdventureLoop
{
    public sealed class VictoryPopUp : GameOverPopUp
    {
        [SerializeField] private Image _splinterImage, _locationImage;
        [SerializeField] private TMP_Text _location, _blueCrystalAmount, _goldCoinAmount;
        
        [Inject] private IAdventureConfigurationService _adventureConfiguration;
        [Inject] private IGameProgressionService _progression;
        [Inject] private IAddressablesService _addressables;
        
        public void Initialize(CurtainTransition curtain)
        {
            SetData(curtain);
                                   
            var locationName = _adventureConfiguration.GetLocation().Header;
            
            _location.text = locationName;
            SetImage(locationName).ManageTaskException();
            
            _blueCrystalAmount.text = AdventureProgression.GetBlueCrystalsBalance().ToString();
            _goldCoinAmount.text = AdventureProgression.GetGoldCoinsBalance().ToString();

            if (!_progression.GetLocationCompleted(locationName))
            {
                AdventureProgression.SetLocationCompleted();
            }
            else
                _splinterImage.color = Color.white * 0.5f;
        }

        private async Task SetImage(string locationName)
        {
            _locationImage.sprite =
                await _addressables.LoadAddrssAsset<Sprite>(locationName);
        }
    }
}
