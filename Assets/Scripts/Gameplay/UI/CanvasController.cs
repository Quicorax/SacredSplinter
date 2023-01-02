using Quicorax;
using TMPro;
using UnityEngine;
public class CanvasController : CanvasWithPopUp
{
    [SerializeField]
    private SimpleEventBus _onResourcesUpdated;

    [SerializeField]
    private UserModel _userProgression;

    [SerializeField]
    private PopUpLauncher _config, _resources;

    [SerializeField]
    private TMP_Text _coinsAmount, _cristalsAmount;

    public void OpenConfiguration() => OnPopUpOpen(_config);
    public void OpenResources() => OnPopUpOpen(_resources);

    private void Start()
    {
        SetItemAmount();
    }

    private void Awake()
    {
        _onResourcesUpdated.Event += SetItemAmount;
    }

    private void OnDestroy()
    {
        _onResourcesUpdated.Event -= SetItemAmount;
    }

    private void SetItemAmount()
    {
        _coinsAmount.text = _userProgression.GetAmountOfResource("Gold Coin").ToString();
        _cristalsAmount.text = _userProgression.GetAmountOfResource("Blue Cristal").ToString();
    }
}
