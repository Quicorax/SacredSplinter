using UnityEngine;
public class MenuCanvasController : WithPopUp
{
    [SerializeField]
    private PopUpLauncher _adventureSelector, _quests, _shop, _library;

    public void OpenAdventureSelector() => OnPopUpOpen(_adventureSelector);
    public void OpenQuests() => OnPopUpOpen(_quests);
    public void OpenShop() => OnPopUpOpen(_shop);
    public void OpenLibrary() => OnPopUpOpen(_library);
}
