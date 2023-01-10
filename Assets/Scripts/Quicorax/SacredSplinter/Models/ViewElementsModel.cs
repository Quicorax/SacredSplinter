using System.Collections.Generic;
using UnityEngine;

namespace Quicorax.SacredSplinter.Models
{
    [CreateAssetMenu(menuName = "Quicorax/Data/ViewElementsModel")]
    public class ViewElementsModel : ScriptableObject
    {
        public List<ViewElement> ViewElement = new();
    }
}