using System;

namespace Quicorax.SacredSplinter.Models
{
    [Serializable]
    public class HeroesData
    {
        public string Name;
        public int MaxHealth;
        public bool CanIgnoreEvents;
        public bool CanEscapeCombats;
    }
}