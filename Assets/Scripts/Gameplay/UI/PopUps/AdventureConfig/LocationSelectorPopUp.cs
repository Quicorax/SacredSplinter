using System;
using UnityEngine;

[Serializable]
public class LocationData: BaseData
{
}

public class LocationSelectorPopUp : SelectorPopUp
{
    [SerializeField]
    private GameObject _artifactCheck;

    private Action<BaseData> _onSelect;
    private Action _onCancel;

    public void Initialize(Action<BaseData> onSelect, Action onCancel)
    {
        _onSelect = onSelect;
        _onCancel = onCancel;
    }

    public void SelectElement()
    {
        _onSelect?.Invoke(Model.Entries[ActualIndex]);

        CloseSelf();
    }

    public override void CloseSelf()
    {
        _onCancel?.Invoke();
        base.CloseSelf();
    }


}