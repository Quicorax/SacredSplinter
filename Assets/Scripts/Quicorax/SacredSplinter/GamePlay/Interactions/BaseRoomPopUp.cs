using System;
using Quicorax.SacredSplinter.MetaGame.UI.PopUps;
using UnityEngine;

namespace Quicorax.SacredSplinter.GamePlay.Interactions
{
    public abstract class BaseRoomPopUp : BasePopUp
    {
        protected int CurrentFloor;

        private Action<int> _onComplete;
        private int _furtherRooms;

        public void SetData(int currentFloor, int furtherRooms, Action<int> onComplete)
        {
            CurrentFloor = currentFloor;
            _furtherRooms = furtherRooms;
            _onComplete = onComplete;

            Initialize();
        }

        protected abstract void Initialize();
        
        protected void Complete()
        {
            _onComplete?.Invoke(_furtherRooms);
            CloseSelf();
        }
    }
}