using System;
using UnityEngine;

public class BasePopUp : MonoBehaviour
{
    private Action _onClosePopUp;
    
    public void Initialize(Action onClosePopUp)
    {
        _onClosePopUp = onClosePopUp;
    }

    public void CloseSelf()
    {
        _onClosePopUp.Invoke();

        Destroy(gameObject);
    }
}
