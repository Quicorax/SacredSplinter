using UnityEngine;

namespace Quicorax.SacredSplinter.Models
{
    [CreateAssetMenu(menuName = "Quicorax/Data/ViewElement")]
    public class ViewElement : ScriptableObject
    {
        public string Element;
        public Sprite Image;
    }
}