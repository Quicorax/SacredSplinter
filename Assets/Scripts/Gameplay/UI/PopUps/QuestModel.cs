using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quicorax/Data/QuestModel")]
public class QuestModel : ScriptableObject
{
    public List<QuestData> Quests = new();
}
