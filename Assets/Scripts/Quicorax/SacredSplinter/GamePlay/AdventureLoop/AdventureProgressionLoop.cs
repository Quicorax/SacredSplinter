using System.Collections.Generic;
using Quicorax.SacredSplinter.GamePlay.Rooms;
using Quicorax.SacredSplinter.Initialization;
using Quicorax.SacredSplinter.MetaGame.UI;
using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using Quicorax.SacredSplinter.Services;
using UnityEngine;

namespace Quicorax.SacredSplinter.GamePlay.AdventureLoop
{
    public class AdventureProgressionLoop : MonoBehaviour
    {
        [SerializeField] private GameCanvasController _gameCanvas;
        [SerializeField] private RoomController _room;
        [SerializeField] private Transform _roomHolder;
        [SerializeField] private PopUpLauncher _victoryPopUp;
        [SerializeField]private CurtainTransition _curtain;

        private readonly List<RoomController> _rooms = new();

        private AdventureProgressionService _adventureProgression;

        public void Initialize()
        {
            _adventureProgression = ServiceLocator.GetService<AdventureProgressionService>();

            InitialRoomPopulation();
        }

        private void InitialRoomPopulation() => PopulateRooms(Random.Range(1, 3));


        private void PopulateRooms(int nextRoomAmount)
        {
            for (var i = 0; i < nextRoomAmount; i++)
            {
                var room = Instantiate(_room, _roomHolder);
                room.Initialize(_adventureProgression.GetCurrentFloor(), DisposeRooms, OnRoomCompleted);

                _rooms.Add(room);
            }
        }

        private void DisposeRooms()
        {
            foreach (var item in _rooms)
                item.Dispose();

            _rooms.Clear();
        }

        private void OnRoomCompleted(int nextRoomAmount)
        {
            _adventureProgression.AddRoom();

            if (_adventureProgression.IsFloorBoosTime())
            {
                if (_adventureProgression.IsLocationBoosTime())
                {
                    //Location boss Event
                    Debug.Log("Floor Boss PopUp");

                    OnLocationBossRoomCompleted();
                    return;
                }

                //Floor boss Event
                Debug.Log("Location Boss PopUp");

                OnFloorBossRoomCompleted();
                return;
            }


            PopulateRooms(nextRoomAmount);
        }

        private void OnFloorBossRoomCompleted()
        {
            _adventureProgression.AddFloor();
            _gameCanvas.UpdateFloorNumber();

            _adventureProgression.ResetRoomCount();

            InitialRoomPopulation();
        }

        private void OnLocationBossRoomCompleted()
        {
            ServiceLocator.GetService<PopUpSpawnerService>().SpawnPopUp<VictoryPopUp>(_victoryPopUp)
                .Initialize(_curtain);
        } 
    }
}