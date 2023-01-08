using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField]
    private FurtherRoomController FurtherRoom;

    [SerializeField]
    private Transform RoomHolder;

    public void Initialize()
    {
        int nextRoomAmount = Random.Range(1, 3);

        for (int i = 0; i < nextRoomAmount; i++)
            Instantiate(FurtherRoom, RoomHolder).Initialize();
    }
}
