using UnityEngine;
using System.Collections.Generic;

namespace Quicorax.SacredSplinter.Models
{
    public class EventsModel : IModel
    {
        public List<EventData> Events = new();

        public EventData GetRandomEvent() => Events[Random.Range(0, Events.Count)];
    }
}