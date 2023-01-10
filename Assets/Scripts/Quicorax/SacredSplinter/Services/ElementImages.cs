using Quicorax.SacredSplinter.Models;
using UnityEngine;

namespace Quicorax.SacredSplinter.Services
{
    public class ElementImages : IService
    {
        private ViewElementsModel _viewElements;

        public void Initialize(ViewElementsModel viewElements)
        {
            _viewElements = viewElements;
        }

        public Sprite GetViewImage(string product)
        {
            foreach (var productView in _viewElements.ViewElement)
            {
                if (product == productView.Element)
                    return productView.Image;
            }

            return null;
        }
    }
}