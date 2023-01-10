namespace Quicorax.SacredSplinter.Services
{
    public class AdventureProgressionService : IService
    {
        private int _maxHealth;
        
        private int _currentHealth;

        public void StartAdventure()
        {
            _currentHealth = _maxHealth;
        }

        public void UpdateHealth(int amount)
        {
            if (_currentHealth + amount > _maxHealth)
            {
                _currentHealth = _maxHealth;
            }
            else if (_currentHealth <= 0)
            {
                PlayerDead();
            }
            else
            {
                _currentHealth += amount;
            }
        }

        private void PlayerDead()
        {
            
        }
    }
}