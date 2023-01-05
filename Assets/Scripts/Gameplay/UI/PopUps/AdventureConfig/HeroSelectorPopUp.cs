using System;
using TMPro;
using UnityEngine;

[Serializable]
public class HeroData : BaseData
{
}

public class HeroSelectorPopUp : SelectorPopUp
{
    [SerializeField]
    private GameObject _selectable;
    [SerializeField]
    private ConfirmHeroUnlockPopUp _unlockHeroPopUp;

    [SerializeField]
    private TMP_Text _selectText;

    private bool _elementUnlocked;

    private GameProgressionService _progress;

    private void Start()
    {
        _progress = ServiceLocator.GetService<GameProgressionService>();

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
    }

    public override void SelectElement()
    {
        if (_elementUnlocked)
        {
            MenuManager.Instance.HeroClassSelected = CurrentElement.Header;
            base.SelectElement();
        }
        else
        {
            ServiceLocator.GetService<PopUpSpawnerService>().SpawnPopUp<ConfirmHeroUnlockPopUp>(_unlockHeroPopUp)
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
