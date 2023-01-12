using Quicorax.SacredSplinter.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Quicorax.SacredSplinter.MetaGame.UI
{
    public class GameCanvasController : MonoBehaviour
    {
        [SerializeField] private Image _background, _hero;
        [SerializeField] private TMP_Text _header, _floorNumber;

        private AdventureConfigurationService _adventureConfig;
        private ElementImagesService _elementImages;

        private void Start()
        {
            _adventureConfig = ServiceLocator.GetService<AdventureConfigurationService>();
            _elementImages = ServiceLocator.GetService<ElementImagesService>();

            SetLevelVisualData();
        }

        private void SetLevelVisualData()
        {
            var location = _adventureConfig.GetLocation();

            _header.text = location;

            _background.sprite = _elementImages.GetViewImage(location);
            _hero.sprite = _elementImages.GetViewImage(_adventureConfig.GetHero());
        }

        public void SetFloorNumber(int number) => _floorNumber.text = number.ToString();
    }
}