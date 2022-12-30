using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quicorax/Data/ProductsModel")]
public class ProductModel : ScriptableObject
{
    public List<ProductData> Products = new();
}