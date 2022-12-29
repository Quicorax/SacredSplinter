using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField]
    private BasePopUp _configPopUp;

    [SerializeField]
    private Button _openPopUpButton;

    private void ActivateButton(bool activate) => _openPopUpButton.interactable = activate;

    public void OpenConfiguration()
    {
        ActivateButton(false);

        Instantiate(_configPopUp, transform).Initialize(OnPopUpClose);
    }

    private void OnPopUpClose()
    {
        ActivateButton(true);
    }
}
