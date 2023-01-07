using System;
using UnityEngine;

[Serializable]
public class ServiceElements
{
    public ViewElementsModel ViewElements;
    public InitialResources InitialResources;
    public Transform PopUpTransformParent;

    public ServiceElements(InitialResources initialResources, Transform popUpTransformParent, ViewElementsModel viewElements)
    {
        InitialResources = initialResources;
        PopUpTransformParent = popUpTransformParent;
        ViewElements = viewElements;
    }
}
