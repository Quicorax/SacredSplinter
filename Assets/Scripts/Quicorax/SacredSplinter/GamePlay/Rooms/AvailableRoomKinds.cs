using System.Collections.Generic;
using UnityEngine;

namespace Quicorax.SacredSplinter.GamePlay.Rooms
{
    [CreateAssetMenu(menuName = "Quicorax/Config/AvailableRooms")]
    public class AvailableRoomKinds : ScriptableObject
    {
        public List<RoomInstance> Rooms = new();
    }
}