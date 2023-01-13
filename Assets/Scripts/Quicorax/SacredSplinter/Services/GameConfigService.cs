using System.Collections.Generic;
using Quicorax.SacredSplinter.Models;

namespace Quicorax.SacredSplinter.Services
{
    public class GameConfigService : IService
    {
        public List<ResourceElement> Resources { get; private set; }
        public List<string> UnlockedHeroes { get; private set; }
        public List<ProgressionOnLevel> LevelsProgression { get; private set; }

        public void Initialize(InitialResources resources)
        {
            Resources = resources.Resources;
            UnlockedHeroes = resources.UnlockedHeroes;
            LevelsProgression = resources.LevelsProgression;
        }
    }
}