using System;

public class BaseRoomPopUp : BasePopUp
{
    private Action<int> _onComplete;

    private int _furtherRooms;

    public void SetData(int furtherRooms, Action<int> onComplete)
    {
        _furtherRooms = furtherRooms;
        _onComplete = onComplete;
    }

    private void Start()
    {
        //Invoke(nameof(Complete), 1f);
    }

    internal void Complete()
    {
        _onComplete?.Invoke(_furtherRooms);
        CloseSelf();
    }
}