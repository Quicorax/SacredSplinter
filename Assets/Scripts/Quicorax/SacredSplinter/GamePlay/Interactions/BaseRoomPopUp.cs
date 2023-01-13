using System;
using Quicorax.SacredSplinter.MetaGame.UI.PopUps;

namespace Quicorax.SacredSplinter.GamePlay.Interactions
{
    public abstract class BaseRoomPopUp : BasePopUp
    {
        private Action<int> _onComplete;

        private int _furtherRooms;

        public void SetData(int furtherRooms, Action<int> onComplete)
        {
            _furtherRooms = furtherRooms;
            _onComplete = onComplete;
        }

        protected void Complete()
        {
            _onComplete?.Invoke(_furtherRooms);
            CloseSelf();
        }
    }
}