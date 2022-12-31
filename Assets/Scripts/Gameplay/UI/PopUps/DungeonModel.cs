using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quicorax/Data/DungeonModel")]
public class DungeonModel : ScriptableObject
{
    public List<DungeonData> Entries = new();
}
