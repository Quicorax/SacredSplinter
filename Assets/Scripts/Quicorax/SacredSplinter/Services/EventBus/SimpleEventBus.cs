using System;
using UnityEngine;

namespace Quicorax.SacredSplinter.Services.EventBus
{
    [CreateAssetMenu(fileName = "SimpleEventBus", menuName = "ScriptableObjects/EventBus/Simple")]
    public class SimpleEventBus : ScriptableObject
    {
        public event Action Event = delegate() { };
        public void NotifyEvent() => Event?.Invoke();
    }
}