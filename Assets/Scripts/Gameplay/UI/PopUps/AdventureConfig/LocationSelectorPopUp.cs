using UnityEngine;

public class LocationSelectorPopUp : HorizontalSelectablePopUp
{
    [SerializeField]
    private GameObject _artifactCheck;

    public void SelectElement()
    {
        OnSelect?.Invoke(Model.Entries[ActualIndex]);

        CloseSelf();
    }
}