using UnityEngine;
using UnityEngine.UI;

public class WithPopUp : MonoBehaviour
{
    public Transform PopUpCanvasTransform;
    public void OnPopUpOpen(PopUpLauncher popUpBundle)
    {
        ActivateButton(popUpBundle.Button, false);
        Instantiate(popUpBundle.PopUp, PopUpCanvasTransform).Initialize(popUpBundle.Button, OnPopUpClose);
    }
    private void OnPopUpClose(Button button) => ActivateButton(button, true);
    private void ActivateButton(Button button, bool activate) => button.interactable = activate;
}
