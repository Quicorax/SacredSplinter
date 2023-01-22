using System;
using System.Linq;
using Quicorax.SacredSplinter.GamePlay.Interactions;
using Quicorax.SacredSplinter.Services;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


namespace Quicorax.SacredSplinter.GamePlay.Rooms
{
    public class RoomController : BaseRoom
    {
        [SerializeField] private AvailableRoomKinds _roomsData;

        [SerializeField] private FurtherRoomController _furtherRoom;

        [SerializeField] private Transform _roomHolder;
        [SerializeField] private Button _selectButton;

        private Action<int> _onRoomComplete;
        private Action _onRoomSelected;

        private int _furtherRooms;
        private int _currentFloor;

        private PopUpSpawnerService _popUpSpawner;
        private AdventureProgressionService _adventureProgression;

        public void Initialize(int currentFloor, Action onRoomSelected, Action<int> onRoomComplete,
            string forceRoom = null)
        {
            _popUpSpawner = ServiceLocator.GetService<PopUpSpawnerService>();
            _adventureProgression = ServiceLocator.GetService<AdventureProgressionService>();

            _currentFloor = currentFloor;
            _onRoomComplete = onRoomComplete;
            _onRoomSelected = onRoomSelected;

            _selectButton.onClick.AddListener(OnSelect);

            SetRoomKind(forceRoom).ManageTaskException();

            _furtherRooms = Random.Range(1, 3);

            if (forceRoom != "Boss" && forceRoom != "LocationBoss")
            {
                for (var i = 0; i < _furtherRooms; i++)
                    Instantiate(_furtherRoom, _roomHolder).Initialize();
            }
        }

        private void OnSelect()
        {
            _onRoomSelected?.Invoke();

            foreach (var item in _roomsData.Rooms.Where(item => item.Kind == RoomKind))
            {
                _adventureProgression.SetCombatType(RoomKind);
                
                _popUpSpawner.SpawnPopUp<BaseRoomPopUp>(item.RoomPopUp)
                    .SetData(_currentFloor, _furtherRooms, _onRoomComplete);
                return;
            }
        }

        public void Dispose() => Destroy(gameObject);
    }
}