﻿using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class SelectablePack
{
    public Image ElementImage;
    public GameObject UnknownElementImage;

    public SelectorPopUp PopUp;
    public Button Button;
}
public class AdventureSelectorPopUp : BasePopUp
{
    [SerializeField]
    private SelectablePack _heroSelector, _dungeonSelector;

    private SelectablePack _actualSelectable;

    [SerializeField]
    private Button _engageOnAdventureButton;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        TurnObjectOn(_dungeonSelector.ElementImage.gameObject, false);
        TurnObjectOn(_heroSelector.ElementImage.gameObject, false);

        ActivateButton(_engageOnAdventureButton, false);
    }
    public void OpenDungeonSelector() => OpenSelectablePopUp(_dungeonSelector);
    public void OpenHeroSelector() => OpenSelectablePopUp(_heroSelector);

    public override void CloseSelf()
    {
        MenuManager.Instance.CancelSelection();
        base.CloseSelf();
    }

    private void OpenSelectablePopUp(SelectablePack selectable)
    {
        _actualSelectable = selectable;

        ActivateButton(selectable.Button, false);
        Instantiate(selectable.PopUp, transform).Initialize(OnPopUpClose, OnSelectableSelected);
    }
    public void EngageOnAdventure()
    {
        StaticNavigation.NavigateToScene("01_Game");
    }

    private void OnSelectableSelected(BaseData data)
    {
        TurnObjectOn(_actualSelectable.ElementImage.gameObject, true);
        TurnObjectOn(_actualSelectable.UnknownElementImage, false);

        _actualSelectable.ElementImage.sprite = data.Image;

        if(MenuManager.Instance.ReadyToEngage())
            ActivateButton(_engageOnAdventureButton, true);
    }

    private void OnPopUpClose() 
    {
        ActivateButton(_actualSelectable.Button, true);
    }
    private void ActivateButton(Button button, bool activate) => button.interactable = activate;
    private void TurnObjectOn(GameObject gameObject, bool on) => gameObject.SetActive(on);
}
