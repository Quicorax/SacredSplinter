using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvasController : MonoBehaviour
{
    [SerializeField]
    private Image _background, _hero;
    [SerializeField]
    private TMP_Text _header, _floorNumber;

    private AdventureConfigurationService _adventureConfig;
    private ImagesService _images;

    private void Start()
    {
        _adventureConfig  = ServiceLocator.GetService<AdventureConfigurationService>();
        _images = ServiceLocator.GetService<ImagesService>();

        SetLevelVisualData();
    }

    private void SetLevelVisualData()
    {
        string location = _adventureConfig.GetLocation();

        _header.text = location;

        _background.sprite = _images.GetViewImage(location);
        _hero.sprite = _images.GetViewImage(_adventureConfig.GetHero());
    }

    public void SetFloorNumber(int number) => _floorNumber.text = number.ToString();
}
