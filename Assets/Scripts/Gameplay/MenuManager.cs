using UnityEngine;
public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public bool HeroSelected;
    public bool DungeonSelected;

    public string HeroClassSelected { set => HeroSelected = true; }
    public string DungeonLocationSelected { set => DungeonSelected = true; }


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void CancelSelection()
    {
        HeroSelected = false;
        DungeonSelected = false;
    }

    public bool ReadyToEngage() => HeroSelected && DungeonSelected;
}
