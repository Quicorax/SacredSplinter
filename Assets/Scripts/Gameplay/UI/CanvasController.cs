using Quicorax;
using TMPro;
using UnityEngine;
public class CanvasController : MonoBehaviour
{
    [SerializeField]
    private SimpleEventBus _onResourcesUpdated;

    [SerializeField]
    private PopUpLauncher _config, _resources;

    [SerializeField]
    private TMP_Text _coinsAmount, _cristalsAmount;

    private PopUpSpawnerService _popUpSpawner;
    private GameProgressionService _gameProgression;

    private void Start()
    {
        _popUpSpawner = ServiceLocator.GetService<PopUpSpawnerService>();
        _gameProgression = ServiceLocator.GetService<GameProgressionService>();

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
    
    public void OpenConfiguration() => _popUpSpawner.SpawnPopUp(_config);
    public void OpenResources() => _popUpSpawner.SpawnPopUp(_resources);

    private void SetItemAmount()
    {
        _coinsAmount.text = _gameProgression.CheckElement("Gold Coin").ToString();
        _cristalsAmount.text = _gameProgression.CheckElement("Blue Cristal").ToString();
    }
}
