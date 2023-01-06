using UnityEngine;
using UnityEngine.UI;

public class PopUpSpawnerService : IService
{
    private Transform _parent;

    public void Initialize(Transform parent)
    {
        _parent = parent;
    }

    public void SpawnPopUp(PopUpLauncher popUpBundle)
    {
        if (popUpBundle.Button != null)
            ActivateButton(popUpBundle.Button, false);

        Object.Instantiate(popUpBundle.PopUp, _parent).Initialize(popUpBundle, x => DeSpawnPopUp(x));
    }

    public T SpawnPopUp<T>(PopUpLauncher popUpBundle) where T : BasePopUp
    {
        if(popUpBundle.Button != null)
            ActivateButton(popUpBundle.Button, false);

        var newPopUp = Object.Instantiate(popUpBundle.PopUp, _parent);
        newPopUp.Initialize(popUpBundle, x => DeSpawnPopUp(x));

        return (T)newPopUp;
    }

    private void DeSpawnPopUp(Button button) 
    {
        if(button != null)
            ActivateButton(button, true);
    }
    private void ActivateButton(Button button, bool activate) => button.interactable = activate;
}