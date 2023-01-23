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

        [SerializeField] private CurtainTransition _curtain;

        private readonly List<RoomController> _rooms = new();

        private AdventureProgressionService _adventureProgression;
        private PopUpSpawnerService _popUpSpawner;

        private bool _nextIsBoss;
        private bool _nextIsLocationBoss;

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
            if (_adventureProgression.GetCurrentHealth() > 0)
            {
                if (_nextIsBoss)
                {
                    _nextIsBoss = false;
                    OnFloorBossRoomCompleted();
                    return;
                }

                if (_nextIsLocationBoss)
                {
                    _nextIsLocationBoss = false;
                    OnLocationBossRoomCompleted();
                    return;
                }
            }

            _adventureProgression.AddRoom();

            if (_adventureProgression.IsFloorBoosTime())
            {
                if (_adventureProgression.IsLocationBoosTime())
                {
                    PopulateRooms(1, "LocationBoss");
                    _nextIsLocationBoss = true;
                    return;
                }

                PopulateRooms(1, "Boss");
                _nextIsBoss = true;
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