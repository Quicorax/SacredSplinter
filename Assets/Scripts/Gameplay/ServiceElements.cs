using System;
using UnityEngine;

[Serializable]
public class ServiceElements
{
    public InitialResources initialResources;
    public Transform popUpTransformParent;

    public ServiceElements(InitialResources initialResources, Transform popUpTransformParent)
    {
        this.initialResources = initialResources;
        this.popUpTransformParent = popUpTransformParent;
    }
}
