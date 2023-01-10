using System.Collections.Generic;
using Quicorax.SacredSplinter.GamePlay.Interactions.Events;
using Quicorax.SacredSplinter.Models;

namespace Quicorax.SacredSplinter.Services
{
    public class EventsModel : IModel
    {
        public List<EventData> Events = new();
    }
}