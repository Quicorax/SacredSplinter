using System;
using System.Collections.Generic;
using Quicorax.SacredSplinter.GamePlay.Interactions.Combat;

namespace Quicorax.SacredSplinter.Models
{
    [Serializable]
    public class HeroData
    {
        public string Header;
        public string Description;
        public bool CanIgnoreEvents;
        public bool CanEscapeCombats;
        public int ExperienceToLvl;

        public string AttackType;
        public string WeaknessType;
        public int AttackAmount;
        
        public int Damage;
        public int DamageEvo;
        
        public int MaxHealth;
        public int HealthEvo;
            
        public int Speed;
        public int SpeedEvo;
        
        public int Agility;
        public int AgilityEvo;
        
        //Dynamic properties
        public List<AttackData> Attacks;
    }
}