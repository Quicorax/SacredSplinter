using System;
using UnityEngine;

namespace Quicorax
{
    [CreateAssetMenu(fileName = "IntEventBus", menuName = "ScriptableObjects/EventBus/Int")]
    public class IntEventBus : ScriptableObject
    {
        public event Action<int> Event = delegate (int i) { };
        public void NotifyEvent(int i) => Event?.Invoke(i);
    }

}
