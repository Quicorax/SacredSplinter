using System;
using Quicorax.SacredSplinter.MetaGame.UI.PopUps;

namespace Quicorax.SacredSplinter.GamePlay.Rooms
{
    [Serializable]
    public class RoomInstance
    {
        public string Kind;
        public PopUpLauncher RoomPopUp;
    }
}