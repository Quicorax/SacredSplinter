using UnityEngine;
using UnityEngine.UI;
public class MenuConfigPopUp : BasePopUp
{
    [SerializeField]
    private BasePopUp _creditsPopUp;

    [SerializeField]
    private Button _openPopUpButton;

    private void ActivateButton(bool activate) => _openPopUpButton.interactable = activate;

    public void OpenCredits()
    {
        ActivateButton(false);

        Instantiate(_creditsPopUp, transform).Initialize(OnPopUpClose);
    }

    public void Save()
    {
        Debug.Log("SAVE");
    }

    private void OnPopUpClose()
    {
        ActivateButton(true);
    }
}
