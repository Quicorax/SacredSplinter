using System;
using UnityEngine;
using UnityEngine.UI;

public class BasePopUp : MonoBehaviour
{
    private Action<Button> _onClosePopUp;
    private Button _laucherButton;
    
    public void Initialize(Button laucherButton, Action<Button> onClosePopUp)
    {
        _onClosePopUp = onClosePopUp;
        _laucherButton = laucherButton;
    }

    public void CloseSelf()
    {
        _onClosePopUp.Invoke(_laucherButton);

        Destroy(gameObject);
    }
}
