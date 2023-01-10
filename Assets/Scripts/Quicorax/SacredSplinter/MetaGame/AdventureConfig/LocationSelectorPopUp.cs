using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using UnityEngine;

namespace Quicorax.SacredSplinter.MetaGame.AdventureConfig
{
    public class LocationSelectorPopUp : HorizontalSelectablePopUp
    {
        [SerializeField] private GameObject _artifactCheck;

        public void SelectElement()
        {
            OnSelect?.Invoke(Model.Entries[ActualIndex]);

            CloseSelf();
        }
    }
}