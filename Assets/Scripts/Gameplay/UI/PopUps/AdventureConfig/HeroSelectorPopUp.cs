using TMPro;
using UnityEngine;

public class HeroSelectorPopUp : SelectorPopUp
{
    [SerializeField]
    private GameObject _selectable;
    [SerializeField]
    private ConfirmHeroUnlockPopUp _unlockHeroPopUp;

    [SerializeField]
    private PopUpLauncher _heroStats;

    [SerializeField]
    private TMP_Text _selectText;

    private bool _elementUnlocked;

    private GameProgressionService _progress;
    private PopUpSpawnerService _popUpSpawner;

    private void Start()
    {
        _progress = ServiceLocator.GetService<GameProgressionService>();
        _popUpSpawner = ServiceLocator.GetService<PopUpSpawnerService>();

        ElementChanged();
    }
       

    public void ElementChanged()
    {
        _elementUnlocked = _progress.CheckHeroUnlocked(CurrentElement.Header);

        _selectable.SetActive(_elementUnlocked);
        _selectText.text = _elementUnlocked ? "Select" : "Unlock";
    }

    public void ShowHeroStats()
    {
        Debug.Log("Open Hero Stats PopUp");

        //TODO: design Hero combat stats system

        //_popUpSpawner.SpawnPopUp(_heroStats); 
    }

    public void SelectElement()
    {
        if (_elementUnlocked)
        {
            OnSelect?.Invoke(Model.Entries[ActualIndex]);
            CloseSelf();
        }
        else
        {
            _popUpSpawner.SpawnPopUp<ConfirmHeroUnlockPopUp>(_unlockHeroPopUp)
                .Initialize(CurrentElement.Header, OnHeroUnlocked);
        }
    }

    private void OnHeroUnlocked()
    {
        _progress.SetAmountOfResource("Hero License", -1);
        _progress.SetHeroUnlocked(CurrentElement.Header);

        ElementChanged();
        SelectElement();
    }
}
