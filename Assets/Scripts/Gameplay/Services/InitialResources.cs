using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quicorax/Config/InitialResources")]
public class InitialResources : ScriptableObject
{
    public List<ResourceElement> Resources = new();
    public List<string> UnlockedHeros = new();
    public List<int> CompletedQuestIndex = new();
    public List<ProgressionOnLevel> LevelsProgression = new();
}
