using UnityEngine;

public class AdventureProgression : MonoBehaviour
{
    [SerializeField]
    private RoomController Room;
    [SerializeField]
    private Transform RoomHolder;

    private void Start()
    {
        Initalize();
    }
    private void Initalize()
    {
        int nextRoomAmount = Random.Range(1, 3);

        for (int i = 0; i < nextRoomAmount; i++)
            Instantiate(Room, RoomHolder).Initialize();
    }
}
