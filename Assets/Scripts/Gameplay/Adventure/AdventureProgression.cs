using System.Collections.Generic;
using UnityEngine;

public class AdventureProgression : MonoBehaviour
{
    [SerializeField]
    private RoomController _room;
    [SerializeField]
    private Transform _roomHolder;

    private List<RoomController> _rooms = new();

    private void Start()
    {
        Initalize();
    }
    private void Initalize()
    {
        PopulateRooms(Random.Range(1, 3));
    }

    private void PopulateRooms(int nextRoomAmount)
    {
        for (int i = 0; i < nextRoomAmount; i++)
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
