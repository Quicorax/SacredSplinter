using UnityEngine;
using UnityEngine.UI;

public interface IWithPopUp
{
    public Transform SpawnParent { get; }
    public void OnPopUpOpen(PopUpLauncher popUpBundle);
    public void OnPopUpClose();
    public void ActivateButton(Button button, bool activate);
}
