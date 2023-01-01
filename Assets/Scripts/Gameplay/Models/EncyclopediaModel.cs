using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quicorax/Data/EncyclopediaModel")]
public class EncyclopediaModel : ScriptableObject
{
    public List<EntryData> Entries = new();
}
