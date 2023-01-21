using System;

namespace Quicorax.SacredSplinter.Models
{
    [Serializable]
    public class EnemyData
    {
        public string Header;
        public string Description;
        public string Location;
        public int ExperienceOnKill;
        
        public int MaxHealth;
        public int HealthEvo;
        
        public int Damage;
        public int DamageEvo;
        
        public int Speed;
        public int SpeedEvo;
        
        //Dynamic properties
        public int CurrentHealth;
        public int CurrentDamage;
        public int CurrentSpeed;
    }
}