using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quicorax/Data/ViewElementsModel")]
public class ViewElementsModel : ScriptableObject
{
    public List<ViewElement> ViewElement = new();
}
