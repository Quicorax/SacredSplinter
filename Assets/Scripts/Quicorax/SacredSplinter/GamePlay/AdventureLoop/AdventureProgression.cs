using System.Collections.Generic;
using Quicorax.SacredSplinter.GamePlay.Rooms;
using UnityEngine;

namespace Quicorax.SacredSplinter.GamePlay.AdventureLoop
{
    public class AdventureProgression : MonoBehaviour
    {
        [SerializeField] private RoomController _room;
        [SerializeField] private Transform _roomHolder;

        private readonly List<RoomController> _rooms = new();

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            PopulateRooms(Random.Range(1, 3));
        }

        private void PopulateRooms(int nextRoomAmount)
        {
            for (var i = 0; i < nextRoomAmount; i++)
            {
                var room = Instantiate(_room, _roomHolder);
                room.Initialize(DisposeRooms, OnRoomCompleted);

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
            PopulateRooms(nextRoomAmount);
        }
    }
}