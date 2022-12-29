using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField]
    private PopUpLauncher _config, _resources;

    public void OpenConfiguration()
    {
        ActivateLauncherButton(_config.Button, false);
        Instantiate(_config.PopUp, transform).Initialize(_config.Button, OnPopUpClose);
    }

    public void OpenResources()
    {
        ActivateLauncherButton(_resources.Button, false);
        Instantiate(_resources.PopUp, transform).Initialize(_resources.Button, OnPopUpClose);
    }

    private void OnPopUpClose(Button button) => ActivateLauncherButton(button, true);
    private void ActivateLauncherButton(Button button, bool activate) => button.interactable = activate;

}
