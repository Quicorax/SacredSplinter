using UnityEngine;
using UnityEngine.UI;

public class CanvasWithPopUp : MonoBehaviour, IWithPopUp
{
    [SerializeField]
    private Transform _spawnParent;
    public Transform SpawnParent => _spawnParent;

    public void OnPopUpOpen(PopUpLauncher popUpBundle)
    {
        ActivateButton(popUpBundle.Button, false);
        Instantiate(popUpBundle.PopUp, SpawnParent).Initialize(popUpBundle.Button, OnPopUpClose);
    }
    public void OnPopUpClose(Button button) => ActivateButton(button, true);
    public void ActivateButton(Button button, bool activate) => button.interactable = activate;
}
