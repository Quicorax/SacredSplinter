using System;
using UnityEngine;

namespace Quicorax.SacredSplinter.Services.EventBus
{
    [CreateAssetMenu(fileName = "StringEventBus", menuName = "ScriptableObjects/EventBus/String")]
    public class StringEventBus : ScriptableObject
    {
        public event Action<string> Event = delegate(string data) { };
        public void NotifyEvent(string data) => Event?.Invoke(data);
    }
}