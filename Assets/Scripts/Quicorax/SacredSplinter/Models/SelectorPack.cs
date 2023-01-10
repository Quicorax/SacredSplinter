using System;
using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using UnityEngine;
using UnityEngine.UI;

namespace Quicorax.SacredSplinter.Models
{
    [Serializable]
    public struct SelectorPack
    {
        public Image Image;
        public GameObject Unselected;
        public PopUpLauncher Launcher;
    }
}