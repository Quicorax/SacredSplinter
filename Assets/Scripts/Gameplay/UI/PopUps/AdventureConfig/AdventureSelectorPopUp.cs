using UnityEngine;
using UnityEngine.UI;

public class AdventureSelectorPopUp : BasePopUp
{
    public Image HeroImage;
    public GameObject UnknownHeroImage;
    public HeroSelectorPopUp HeroSelector;
    public Button HeroButton;

    public Image LocationImage;
    public GameObject UnknownLocationImage;
    public LocationSelectorPopUp LocationSelector;
    public Button LocationButton;

    [SerializeField]
    private Button _engageOnAdventureButton;

    private PopUpSpawnerService _popUpSpawner;
    private AdventureConfigurationService _adventureConfig;


    private void Start()
    {
        _popUpSpawner = ServiceLocator.GetService<PopUpSpawnerService>();
        _adventureConfig = ServiceLocator.GetService<AdventureConfigurationService>();

        Initialize();
    }

    public void Initialize()
    {
        _adventureConfig.ResetSelection();

        TurnObjectOn(LocationImage.gameObject, false);
        TurnObjectOn(HeroImage.gameObject, false);

        ActivateButton(_engageOnAdventureButton, false);
    }
    public void OpenLocationSelector()
    {
        ActivateButton(LocationButton, false);

        _popUpSpawner.SpawnPopUp<LocationSelectorPopUp>(LocationSelector)
            .Initialize(OnLocationSelected, OnLocationSelectionCancelled);
    }
    public void OpenHeroSelector()
    {
        ActivateButton(HeroButton, false);

        _popUpSpawner.SpawnPopUp<HeroSelectorPopUp>(HeroSelector)
            .Initialize(OnHeroSelected, OnHeroSelectionCancelled);
    }

    public void EngageOnAdventure()
    {
        CloseSelf();
        ServiceLocator.GetService<NavigationService>().NavigateToScene("01_Game");
    }

    private void OnLocationSelected(BaseData data)
    {
        _adventureConfig.SetLocation(data.Header);

        LocationImage.sprite = data.Image;

        TurnObjectOn(LocationImage.gameObject, true);
        TurnObjectOn(UnknownLocationImage, false);

        ActivateButton(LocationButton, true);

        CheckReadyToEngage();
    }
    private void OnHeroSelected(BaseData data)
    {
        _adventureConfig.SetHero(data.Header);

        HeroImage.sprite = data.Image;

        TurnObjectOn(HeroImage.gameObject, true);
        TurnObjectOn(UnknownHeroImage, false);

        ActivateButton(HeroButton, true);

        CheckReadyToEngage();
    }

    private void OnHeroSelectionCancelled() => ActivateButton(HeroButton, true);
    private void OnLocationSelectionCancelled() => ActivateButton(LocationButton, true);

    private void CheckReadyToEngage()
    {
        if (_adventureConfig.ReadyToEngage())
            ActivateButton(_engageOnAdventureButton, true);
    }
    public void CancelSelection() => _adventureConfig.ResetSelection();

    private void ActivateButton(Button button, bool activate) => button.interactable = activate;
    private void TurnObjectOn(GameObject gameObject, bool on) => gameObject.SetActive(on);
}
