using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuConfigPopUp : BaseConfigPopUp
{
    [SerializeField]
    private TMP_Text _languageDisplay;

    [SerializeField]
    private PopUpLauncher _credits;

    public void OpenCredits()
    {
        ActivateButton(_credits.Button, false);
        Instantiate(_credits.PopUp, transform).Initialize(_credits.Button, OnPopUpClose);
    }

    public void Save()
    {
        Debug.Log("SAVE");
    }

    public void ChangeLanguage(bool next)
    {
        Debug.Log("LANGUAGE");
    }

    private void OnPopUpClose(Button button) => ActivateButton(button, true);
    private void ActivateButton(Button button, bool activate) => button.interactable = activate;
}