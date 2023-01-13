using System.Collections.Generic;
using UnityEngine;

namespace Quicorax.SacredSplinter.Models
{
    [CreateAssetMenu(menuName = "Quicorax/Config/InitialResources")]
    public class InitialResources : ScriptableObject
    {
        public List<ResourceElement> Resources = new();
        public List<string> UnlockedHeroes = new();
        public List<int> CompletedQuestIndex = new();
        public List<ProgressionOnLevel> LevelsProgression = new();
    }
}