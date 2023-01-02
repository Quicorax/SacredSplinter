using System;
using UnityEngine;

namespace Quicorax
{
    [CreateAssetMenu(fileName = "Vector3EventBus", menuName = "ScriptableObjects/EventBus/Vector3")]
    public class Vector3EventBus : ScriptableObject
    {
        public event Action<Vector3> Event = delegate (Vector3 vector) { };
        public void NotifyEvent(Vector3 vector) => Event?.Invoke(vector);
    }
}
