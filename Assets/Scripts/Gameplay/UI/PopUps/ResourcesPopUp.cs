using TMPro;
using UnityEngine;

public class ResourcesPopUp : BasePopUp
{
    [SerializeField]
    private UserModel _userProgression;

    [SerializeField]
    private TMP_Text _cristalsAmount, _coinsAmount, _heroLicense;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _coinsAmount.text = _userProgression.GetAmountOfResource("Gold Coin").ToString();
        _cristalsAmount.text = _userProgression.GetAmountOfResource("Blue Cristal").ToString();
        _heroLicense.text = _userProgression.GetAmountOfResource("Hero License").ToString();
    }
}
