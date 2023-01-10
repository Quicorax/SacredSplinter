using System;
using System.Collections.Generic;
using System.Linq;
using Quicorax.SacredSplinter.GamePlay.Interactions;
using Quicorax.SacredSplinter.Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Quicorax.SacredSplinter.GamePlay.Rooms
{
    public class RoomController : BaseRoom
    {
        [SerializeField] private List<RoomInstance> _roomInstance = new();

        [SerializeField] private FurtherRoomController _furtherRoom;

        [SerializeField] private Transform _roomHolder;

        private Action<int> _onRoomComplete;
        private Action _onRoomSelected;

        private int _furtherRooms;

        private PopUpSpawnerService _popUpSpawner;

        public void Initialize(Action onRoomSelected, Action<int> onRoomComplete)
        {
            _popUpSpawner = ServiceLocator.GetService<PopUpSpawnerService>();
            _onRoomComplete = onRoomComplete;
            _onRoomSelected = onRoomSelected;

            SetRoomKind();

            _furtherRooms = Random.Range(1, 3);

            for (var i = 0; i < _furtherRooms; i++)
                Instantiate(_furtherRoom, _roomHolder).Initialize();
        }

        public void OnSelect()
        {
            _onRoomSelected?.Invoke();

            foreach (var item in _roomInstance.Where(item => item.Kind == RoomKind))
            {
                _popUpSpawner.SpawnPopUp<BaseRoomPopUp>(item.RoomPopUp).SetData(_furtherRooms, _onRoomComplete);
                return;
            }
        }

        public void Dispose() => Destroy(gameObject);
    }
}