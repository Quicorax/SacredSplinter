using System;

namespace Quicorax.SacredSplinter.Models
{
    [Serializable]
    public class QuestData
    {
        public int Index;
        public string Concept;

        public string Header;
        public int Amount;

        public string Reward;
        public int RewardAmount;
    }
}