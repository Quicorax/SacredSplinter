using System;
using UnityEngine.Serialization;

namespace Quicorax.SacredSplinter.Models
{
    [Serializable]
    public class HeroesData
    {
        public string Header;
        public string Description;
        public int MaxHealth;
        public bool CanIgnoreEvents;
        public bool CanEscapeCombats;
    }
}