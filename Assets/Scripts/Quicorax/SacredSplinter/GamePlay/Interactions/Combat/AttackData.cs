using System;

namespace Quicorax.SacredSplinter.GamePlay.Interactions.Combat
{
    [Serializable]
    public class AttackData
    {
        public string Header;
        public string AttackType;
        
        public int Damage;
        public int CooldownTurns;
    }
}