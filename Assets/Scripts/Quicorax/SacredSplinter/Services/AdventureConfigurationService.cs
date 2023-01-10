namespace Quicorax.SacredSplinter.Services
{
    public class AdventureConfigurationService : IService
    {
        private string _hero, _location = string.Empty;

        public void SetHero(string hero) => _hero = hero;
        public void SetLocation(string location) => _location = location;
        public string GetHero() => _hero;
        public string GetLocation() => _location;
        public bool ReadyToEngage() => _hero != string.Empty && _location != string.Empty;

        public void ResetSelection()
        {
            _hero = string.Empty;
            _location = string.Empty;
        }
    }
}