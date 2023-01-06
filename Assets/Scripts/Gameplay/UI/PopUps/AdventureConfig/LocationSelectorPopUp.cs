using UnityEngine;

public class LocationSelectorPopUp : SelectorPopUp
{
    [SerializeField]
    private GameObject _artifactCheck;

    public void SelectElement()
    {
        OnSelect?.Invoke(Model.Entries[ActualIndex]);

        CloseSelf();
    }
}