using UnityEngine;
public class CanvasController : CanvasWithPopUp
{
    [SerializeField]
    private PopUpLauncher _config, _resources;

    public void OpenConfiguration() => OnPopUpOpen(_config);
    public void OpenResources() => OnPopUpOpen(_resources);
}
