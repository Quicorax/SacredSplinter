using System;
using UnityEngine;

[Serializable]
public class DungeonData: BaseData
{
}

public class DungeonSelectorPopUp : SelectorPopUp
{
    [SerializeField]
    private GameObject _artifactCheck;

    public override void SelectElement()
    {
        MenuManager.Instance.DungeonLocationSelected = CurrentElement.Header;
        base.SelectElement();
    }
}