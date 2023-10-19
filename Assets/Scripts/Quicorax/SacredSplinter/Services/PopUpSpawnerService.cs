using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using UnityEngine;
using UnityEngine.UI;

namespace Quicorax.SacredSplinter.Services
{
    public interface IPopUpSpawnerService
    {
        void Initialize(Transform parent);
        void SpawnPopUp(PopUpLauncher popUpBundle);
        T SpawnPopUp<T>(PopUpLauncher popUpBundle) where T : BasePopUp;
    }

    public class PopUpSpawnerService : IPopUpSpawnerService

    {
    private Transform _parent;

    public void Initialize(Transform parent) => _parent = parent;

    public void SpawnPopUp(PopUpLauncher popUpBundle)
    {
        if (popUpBundle.Button != null)
            ActivateButton(popUpBundle.Button, false);

        Object.Instantiate(popUpBundle.PopUp, _parent).BaseInitialize(popUpBundle, DeSpawnPopUp);
    }

    public T SpawnPopUp<T>(PopUpLauncher popUpBundle) where T : BasePopUp
    {
        if (popUpBundle.Button != null)
        {
            ActivateButton(popUpBundle.Button, false);
        }

        var newPopUp = Object.Instantiate(popUpBundle.PopUp, _parent);
        newPopUp.BaseInitialize(popUpBundle, DeSpawnPopUp);

        return (T)newPopUp;
    }

    private void DeSpawnPopUp(Button button)
    {
        if (button != null)
            ActivateButton(button, true);
    }

    private void ActivateButton(Button button, bool activate) => button.interactable = activate;
    }
}