using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomController : BaseRoom
{
    [SerializeField]
    private List<RoomInstance> _roomInstance = new();

    [SerializeField]
    private FurtherRoomController FurtherRoom;

    [SerializeField]
    private Transform RoomHolder;

    private Action<int> _onRoomComplete;
    private Action _onRoomSelected;

    private int _furtherRooms;

    public void Initialize(Action onRoomSelected, Action<int> onRoomComplete)
    {
        _onRoomComplete = onRoomComplete;
        _onRoomSelected = onRoomSelected;

        SetRoomKind();

        _furtherRooms = Random.Range(1, 3);

        for (int i = 0; i < _furtherRooms; i++)
            Instantiate(FurtherRoom, RoomHolder).Initialize();
    }

    public override void OnSelect()
    {
        _onRoomSelected?.Invoke();

        foreach (var item in _roomInstance)
        {
            if (item.Kind == RoomKind)
            {
                ServiceLocator.GetService<PopUpSpawnerService>()
                    .SpawnPopUp<BaseRoomPopUp>(item.RoomPopUp).SetData(_furtherRooms, _onRoomComplete);

                return;
            }
        }
    }

    public void Dispose() => Destroy(gameObject);
}
