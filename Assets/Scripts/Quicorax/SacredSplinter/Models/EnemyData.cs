using System;

namespace Quicorax.SacredSplinter.Models
{
    [Serializable]
    public class EnemyData
    {
        public string Header;
        public string Description;
        
        public string Type;
        
        public string Location;
        public int MinFloor;
        
        public string AttackType;
        public int AttackAmount;
        
        public int MaxHealth;
        public int HealthEvo;
        
        public int Damage;
        public int DamageEvo;
        
        public int Speed;
        public int SpeedEvo;
        
        public int ExperienceOnKill;
        public int BlueCrystalsOnKill;
        public int GoldCoinsOnKill;
        
        //Dynamic properties
        public int CurrentHealth;
        public int CurrentDamage;
        public int CurrentSpeed;
        
        public int TempBlueCrystalReward;
        public int TempGoldCoinReward;
    }
}