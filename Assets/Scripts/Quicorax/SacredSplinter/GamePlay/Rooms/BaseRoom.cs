using System.Collections.Generic;
using System.Threading.Tasks;
using Quicorax.SacredSplinter.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

namespace Quicorax.SacredSplinter.GamePlay.Rooms
{
    public abstract class BaseRoom : MonoBehaviour
    {
        [SerializeField] private Image _roomIcon;
        
        [Inject] private IAddressablesService _addressables;
        
        protected string RoomKind;

        private readonly List<string> _randomRoomKinds = new()
        {
            "Combat",
            "Event",
        };

        protected async Task SetRoomKind(string forceKind = null)
        {
            RoomKind = !string.IsNullOrEmpty(forceKind) ? forceKind : ChooseRandomKind();
            _roomIcon.sprite = await _addressables.LoadAddrssAsset<Sprite>($"{RoomKind}_Icon");
        }

        private string ChooseRandomKind() => _randomRoomKinds[Random.Range(0, _randomRoomKinds.Count)];
    }
}