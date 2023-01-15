using System.Collections.Generic;
using System.Threading.Tasks;
using Quicorax.SacredSplinter.Services;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Quicorax.SacredSplinter.GamePlay.Rooms
{
    public abstract class BaseRoom : MonoBehaviour
    {
        [SerializeField] private Image _roomIcon;

        internal string RoomKind;

        private readonly List<string> _roomKinds = new()
        {
            "Combat",
            "Event",
            //"Unknown",
            //"Boss",
            //"Shop", (?)
        };

        protected async Task SetRoomKind(string forceKind = null)
        {
            RoomKind = !string.IsNullOrEmpty(forceKind) ? forceKind : ChooseRandomKind();
            _roomIcon.sprite = await ServiceLocator.GetService<AddressablesService>().LoadAddrssAsset<Sprite>(RoomKind);
        }

        private string ChooseRandomKind() => _roomKinds[Random.Range(0, _roomKinds.Count)];
    }
}