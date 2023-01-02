using System;
using UnityEngine;

[Serializable]
public class HeroData : BaseData
{ 
}

public class HeroSelectorPopUp : SelectorPopUp
{
   // Locked/Unlocked Logic

    public void ShowHeroStats()
    {

    }

    public override void SelectElement()
    {
        MenuManager.Instance.HeroClassSelected = CurrentElement.Header;
        base.SelectElement();
    }
}
