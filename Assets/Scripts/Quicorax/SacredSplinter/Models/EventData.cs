using System;

namespace Quicorax.SacredSplinter.Models
{
    [Serializable]
    public class EventData
    {
        public string Concept;

        public string Header;
        public string Action;
        public int Chance;

        public string FailHeader;
        public string FailKind;
        public int FailAmount;

        public string SuccedHeader;
        public string SuccedKind;
        public int SuccedMinAmount;
        public int SuccedMaxAmount;
    }
}