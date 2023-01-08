using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[Serializable]
public class RoomInstance
{
    public string Kind;
    public PopUpLauncher RoomPopUp;
}
public class BaseRoom : MonoBehaviour
{
    [SerializeField]
    private Image _roomIcon;

    internal string RoomKind;

    private List<string> _roomKinds = new()
    {
        "Combat",
        "Event",
        //"Unknown",
        //"Boss",
        //"Shop", (?)
    };

    public void SetRoomKind(string forceKind = null)
    {
        if (forceKind != null && forceKind != string.Empty)
            RoomKind = forceKind;
        else
            RoomKind = ChooseRandomKind();

        _roomIcon.sprite = ServiceLocator.GetService<ImagesService>().GetViewImage(RoomKind);
    }

    private string ChooseRandomKind() => _roomKinds[Random.Range(0, _roomKinds.Count)];

    public virtual void OnSelect() { }
}
