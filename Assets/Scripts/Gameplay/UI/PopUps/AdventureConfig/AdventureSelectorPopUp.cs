using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct SelectorPack
{
    public Image Image;
    public GameObject Unselected;
    public PopUpLauncher Launcher;
}
public class AdventureSelectorPopUp : BasePopUp
{
    [SerializeField]
    private SelectorPack _heroSelectionPack, _locationSelectionPack;

    [SerializeField]
    private Button _engageOnAdventureButton;

    private PopUpSpawnerService _popUpSpawner;
    private AdventureConfigurationService _adventureConfig;

    public void Initialize()
    {
        _popUpSpawner = ServiceLocator.GetService<PopUpSpawnerService>();
        _adventureConfig = ServiceLocator.GetService<AdventureConfigurationService>();

        _adventureConfig.ResetSelection();

        TurnObjectOn(_locationSelectionPack.Image.gameObject, false);
        TurnObjectOn(_heroSelectionPack.Image.gameObject, false);

        ActivateButton(_engageOnAdventureButton, false);
    }

    public void EngageOnAdventure()
    {
        CloseSelf();
        ServiceLocator.GetService<NavigationService>().NavigateToScene("01_Game");
    }


    public void OpenLocationSelector() => 
        OpenSelector<LocationSelectorPopUp>(x => _adventureConfig.SetLocation(x.Header), _locationSelectionPack);

    public void OpenHeroSelector() => 
        OpenSelector<HeroSelectorPopUp>(x => _adventureConfig.SetHero(x.Header), _heroSelectionPack);

    private void OpenSelector<T>(Action<BaseData> setData,SelectorPack elements) where T: HorizontalSelectablePopUp
    {
        ActivateButton(elements.Launcher.Button, false);
        _popUpSpawner.SpawnPopUp<T>(elements.Launcher)
            .Initialize(data => OnSelectionSucced(setData, data, elements), ()=> OnSelectionFailed(elements));
    }

    private void OnSelectionSucced(Action<BaseData> setData, BaseData data, SelectorPack elements)
    {
        setData?.Invoke(data);

        elements.Image.sprite = data.Image;

        TurnObjectOn(elements.Image.gameObject, true);
        TurnObjectOn(elements.Unselected, false);

        ActivateButton(elements.Launcher.Button, true);

        if (_adventureConfig.ReadyToEngage())
            ActivateButton(_engageOnAdventureButton, true);
    }

    private void OnSelectionFailed(SelectorPack elements) => ActivateButton(elements.Launcher.Button, true);

    public void CancelSelection() => _adventureConfig.ResetSelection();

    private void ActivateButton(Button button, bool activate) => button.interactable = activate;
    private void TurnObjectOn(GameObject gameObject, bool on) => gameObject.SetActive(on);
}
