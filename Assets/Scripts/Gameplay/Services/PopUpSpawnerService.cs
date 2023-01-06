using System;
using UnityEngine;
using UnityEngine.UI;

public class PopUpSpawnerService : IService
{
    private Button _launcherButton;

    private Transform _parent;

    public void Initialize(Transform parent)
    {
        _parent = parent;
    }
    public void SpawnPopUp(PopUpLauncher popUpBundle)
    {
        _launcherButton = popUpBundle.Button;

        ActivateButton(popUpBundle.Button, false);

        UnityEngine.Object.Instantiate(popUpBundle.PopUp, _parent).BaseInitialize(DeSpawnPopUp);
    }
    public T SpawnPopUp<T>(BasePopUp popUp) where T : BasePopUp
    {
        var newPopUp = UnityEngine.Object.Instantiate(popUp, _parent);
        newPopUp.BaseInitialize(DeSpawnPopUp);

        return (T)newPopUp;
    }

    public void SpawnPopUp(BasePopUp popUp, Action action)
    {
        UnityEngine.Object.Instantiate(popUp, _parent).BaseInitialize(action);
    }

    private void DeSpawnPopUp() => ActivateButton(_launcherButton, true);
    private void ActivateButton(Button button, bool activate) => button.interactable = activate;
}