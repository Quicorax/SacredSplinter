using UnityEngine;
using UnityEngine.UI;

public class AdventureSelectorPopUp : BasePopUp
{
    [SerializeField]
    private Image _dungeonImage, _heroImage;

    [SerializeField]
    private PopUpLauncher _heroSelector, _dungeonSelector;

    public void OpenHeroSelector() => OnPopUpOpen(_heroSelector);
    public void OpenDungeonSelector() => OnPopUpOpen(_dungeonSelector);

    private void OnPopUpOpen(PopUpLauncher popUpBundle)
    {
        ActivateButton(popUpBundle.Button, false);
        Instantiate(popUpBundle.PopUp, transform).Initialize(popUpBundle.Button, OnPopUpClose);
    }
    private void OnPopUpClose(Button button) => ActivateButton(button, true);
    private void ActivateButton(Button button, bool activate) => button.interactable = activate;

    public void EngageOnAdventure()
    {
        StaticNavigation.NavigateToScene("01_Game");
    }
}
