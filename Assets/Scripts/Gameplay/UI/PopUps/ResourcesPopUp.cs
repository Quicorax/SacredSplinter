using TMPro;
using UnityEngine;

public class ResourcesPopUp : BasePopUp
{
    [SerializeField]
    private TMP_Text _cristalsAmount, _coinsAmount, _heroLicense;

    private GameProgressionService _progression;
    private void Start()
    {
        _progression = ServiceLocator.GetService<GameProgressionService>();

        Initialize();
    }

    private void Initialize()
    {
        _coinsAmount.text = _progression.CheckAmountOfResource("Gold Coin").ToString();
        _cristalsAmount.text = _progression.CheckAmountOfResource("Blue Cristal").ToString();
        _heroLicense.text = _progression.CheckAmountOfResource("Hero License").ToString();
    }
}
