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

        Object.Instantiate(popUpBundle.PopUp, _parent).Initialize(DeSpawnPopUp);
    }

    private void DeSpawnPopUp() => ActivateButton(_launcherButton, true);
    private void ActivateButton(Button button, bool activate) => button.interactable = activate;
}