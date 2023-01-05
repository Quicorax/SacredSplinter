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
public class MenuManager : MonoBehaviour
{
    [SerializeField]
    ServiceElements _servicesElements;

    public static MenuManager Instance;

    public bool HeroSelected;
    public bool DungeonSelected;

    public string HeroClassSelected { set => HeroSelected = true; }
    public string DungeonLocationSelected { set => DungeonSelected = true; }


    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        InitializeServiceLocator();
    }

    private void InitializeServiceLocator()
    {
        ServiceFeeder serviceLoader = new();

        serviceLoader.LoadSevices(_servicesElements);
    }

    public void CancelSelection()
    {
        HeroSelected = false;
        DungeonSelected = false;
    }

    public bool ReadyToEngage() => HeroSelected && DungeonSelected;
}
