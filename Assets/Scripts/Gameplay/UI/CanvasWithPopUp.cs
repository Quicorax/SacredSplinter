using UnityEngine;
using UnityEngine.UI;

public class CanvasWithPopUp : MonoBehaviour, IWithPopUp
{
    [SerializeField]
    private Transform _spawnParent;
    public Transform SpawnParent => _spawnParent;

    private Button _launcherButton;

    public void OnPopUpOpen(PopUpLauncher popUpBundle)
    {
        _launcherButton = popUpBundle.Button;

        ActivateButton(popUpBundle.Button, false);
        Instantiate(popUpBundle.PopUp, SpawnParent).Initialize(OnPopUpClose);
    }
    public void OnPopUpClose() => ActivateButton(_launcherButton, true);
    public void ActivateButton(Button button, bool activate) => button.interactable = activate;
}
