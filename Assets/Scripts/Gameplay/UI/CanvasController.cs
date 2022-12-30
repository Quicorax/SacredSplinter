using UnityEngine;
public class CanvasController : WithPopUp
{
    [SerializeField]
    private PopUpLauncher _config, _resources;

    public void OpenConfiguration() => OnPopUpOpen(_config);
    public void OpenResources() => OnPopUpOpen(_resources);
}
