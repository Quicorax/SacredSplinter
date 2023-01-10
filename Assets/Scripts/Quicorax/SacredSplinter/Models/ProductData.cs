using System;

namespace Quicorax.SacredSplinter.Models
{
    [Serializable]
    public class ProductData
    {
        public string Header;

        public string Price;
        public int PriceAmount;

        public string Reward;
        public int RewardAmount;
    }
}