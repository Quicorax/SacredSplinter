using UnityEngine;
using UnityEngine.UI;
public class MenuCanvasController : MonoBehaviour
{
    [SerializeField]
    private PopUpLauncher _adventureSelector;

    public void OpenAdventureSelector()
    {
        ActivateButton(_adventureSelector.Button, false);
        Instantiate(_adventureSelector.PopUp, transform).Initialize(_adventureSelector.Button, OnPopUpClose);
    }

    private void OnPopUpClose(Button button) => ActivateButton(button, true);
    private void ActivateButton(Button button, bool activate) => button.interactable = activate;

}
