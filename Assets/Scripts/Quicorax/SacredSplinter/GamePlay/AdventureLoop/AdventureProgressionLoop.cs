using System.Collections.Generic;
using Quicorax.SacredSplinter.GamePlay.Interactions.Combat;
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
        [SerializeField] private PopUpLauncher _boosCombatPopUp;

        [SerializeField] private CurtainTransition _curtain;

        private readonly List<RoomController> _rooms = new();

        private AdventureProgressionService _adventureProgression;
        private PopUpSpawnerService _popUpSpawner;

        public void Initialize()
        {
            _adventureProgression = ServiceLocator.GetService<AdventureProgressionService>();
            _popUpSpawner = ServiceLocator.GetService<PopUpSpawnerService>();

            InitialRoomPopulation();
        }

        private void InitialRoomPopulation() => PopulateRooms(Random.Range(1, 3));


        private void PopulateRooms(int nextRoomAmount, string forceRoom = null)
        {
            for (var i = 0; i < nextRoomAmount; i++)
            {
                var room = Instantiate(_room, _roomHolder);
                room.Initialize(_adventureProgression.GetCurrentFloor(), DisposeRooms, OnRoomCompleted, forceRoom);

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
                    //_popUpSpawner.SpawnPopUp<BossCombatPopUp>(_boosCombatPopUp).Initialize();
                    PopulateRooms(1, "Boss");

                    //OnLocationBossRoomCompleted();
                    return;
                }

                //Floor boss Event
                //_popUpSpawner.SpawnPopUp<BossCombatPopUp>(_boosCombatPopUp).Initialize();
                PopulateRooms(1, "Boss");

                //OnFloorBossRoomCompleted();
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
            _popUpSpawner.SpawnPopUp<VictoryPopUp>(_victoryPopUp)
                .Initialize(_curtain);
        }
    }
}