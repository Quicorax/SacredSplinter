using Quicorax.SacredSplinter.Services;

namespace Quicorax.SacredSplinter.GamePlay.Rooms
{
    public class FurtherRoomController : BaseRoom
    {
        public void Initialize()
        {
            SetRoomKind("Unknown").ManageTaskException();
        }
    }
}