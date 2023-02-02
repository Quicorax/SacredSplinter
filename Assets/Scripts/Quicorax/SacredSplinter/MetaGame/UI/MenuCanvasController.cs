using Quicorax.SacredSplinter.Initialization;
using Quicorax.SacredSplinter.MetaGame.AdventureConfig;
using Quicorax.SacredSplinter.MetaGame.Encyclopedia;
using Quicorax.SacredSplinter.MetaGame.Quests;
using Quicorax.SacredSplinter.MetaGame.Shop;
using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Services;
using UnityEngine;

namespace Quicorax.SacredSplinter.MetaGame.UI
{
    public class MenuCanvasController : MonoBehaviour
    {
        private PopUpSpawnerService _popUpSpawner;

        [SerializeField] private PopUpLauncher _adventureSelector, _quests, _shop, _encyclopedia;

        [SerializeField] private CurtainTransition _curtain;

        private void Start()
        {
            _popUpSpawner = ServiceLocator.GetService<PopUpSpawnerService>();

            SetButtonsListener();
        }

        private void SetButtonsListener()
        {
            _quests.Button.onClick.AddListener(OpenQuests);
            _shop.Button.onClick.AddListener(OpenShop);
            _encyclopedia.Button.onClick.AddListener(OpenEncyclopedia);
            _adventureSelector.Button.onClick.AddListener(OpenAdventureSelector);
        }

        private void OpenQuests() => _popUpSpawner.SpawnPopUp<QuestsPopUp>(_quests).Initialize();
        private void OpenShop() => _popUpSpawner.SpawnPopUp<ShopPopUp>(_shop).Initialize();
        private void OpenEncyclopedia() => _popUpSpawner.SpawnPopUp<EncyclopediaPopUp>(_encyclopedia).Initialize();

        private void OpenAdventureSelector() =>
            _popUpSpawner.SpawnPopUp<AdventureSelectorPopUp>(_adventureSelector).Initialize(EngageOnAdventure);

        private void EngageOnAdventure() =>
            _curtain.CurtainOn(() => ServiceLocator.GetService<NavigationService>().NavigateToGame());
    }
}