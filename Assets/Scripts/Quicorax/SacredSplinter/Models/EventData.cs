using System;

namespace Quicorax.SacredSplinter.Models
{
    [Serializable]
    public class EventData
    {
        public bool Active;
        
        public string Concept;
        
        public string Location;

        public string Header;
        public string Action;
        public int Chance;

        public string SuccedHeader;
        public string SuccedKind;
        public int SuccedMinAmount;
        public int SuccedMaxAmount;
        
        public string FailHeader;
        public string FailKind;
        public int FailAmount;
        
        public string DeathMotive;
    }
}