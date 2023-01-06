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

    private Action<BaseData> _onSelect;
    private Action _onCancel;

    private void Start()
    {
        _progress = ServiceLocator.GetService<GameProgressionService>();

        ElementChanged();
    }

    public void Initialize(Action<BaseData> onSelect, Action onCancel)
    {
        _onSelect = onSelect;
        _onCancel = onCancel;
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

    public void SelectElement()
    {
        if (_elementUnlocked)
        {
            _onSelect?.Invoke(Model.Entries[ActualIndex]);
            CloseSelf();
        }
        else
        {
            ServiceLocator.GetService<PopUpSpawnerService>().SpawnPopUp<ConfirmHeroUnlockPopUp>(_unlockHeroPopUp)
                .Initialize(CurrentElement.Header, OnHeroUnlocked);
        }
    }

    public override void CloseSelf()
    {
        _onCancel?.Invoke();
        base.CloseSelf();
    }

    private void OnHeroUnlocked()
    {
        _progress.SetAmountOfResource("Hero License", -1);
        _progress.SetHeroUnlocked(CurrentElement.Header);

        ElementChanged();
        SelectElement();
    }
}
