using System;
using TMPro;
using UnityEngine;

[Serializable]
public class EntryData : BaseData
{
    public string Location;
}

public class EncyclopediaPopUp : SelectorPopUp
{
    [SerializeField]
    private TMP_Text _location;

}