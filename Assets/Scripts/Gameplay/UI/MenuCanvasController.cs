using UnityEngine;
using UnityEngine.UI;

public class MenuCanvasController : MonoBehaviour
{
    [SerializeField]
    private BasePopUp _adventureSelectorPopUp;

    [SerializeField]
    private Button _openPopUpButton;

    private void ActivateButton(bool activate) => _openPopUpButton.interactable = activate;

    public void OpenAdventureSelector()
    {
        ActivateButton(false);

        Instantiate(_adventureSelectorPopUp, transform).Initialize(OnPopUpClose);
    }

    private void OnPopUpClose()
    {
        ActivateButton(true);
    }
}
