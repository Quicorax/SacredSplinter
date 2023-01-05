using TMPro;
using UnityEngine;

public class MenuConfigPopUp : BaseConfigPopUp
{
    [SerializeField]
    private TMP_Text _languageDisplay;

    [SerializeField]
    private PopUpLauncher _credits;

    public void OpenCredits() => ServiceLocator.GetService<PopUpSpawnerService>().SpawnPopUp(_credits);

    public void Save() => ServiceLocator.GetService<SaveLoadService>().Save();

    public void ChangeLanguage(bool next)
    {
        Debug.Log("LANGUAGE");
    }
}