using UnityEngine;
public class MenuCanvasController : CanvasWithPopUp
{
    [SerializeField]
    private PopUpLauncher _adventureSelector, _quests, _shop, _encyclopedia;

    public void OpenAdventureSelector() => OnPopUpOpen(_adventureSelector);
    public void OpenQuests() => OnPopUpOpen(_quests);
    public void OpenShop() => OnPopUpOpen(_shop);
    public void OpenEncyclopedia() => OnPopUpOpen(_encyclopedia);
}
