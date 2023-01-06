using TMPro;
using UnityEngine;

public class GameCanvasController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _hero, _location;

    private void Start()
    {
        var adventureConfig = ServiceLocator.GetService<AdventureConfigurationService>();
        
        _hero.text = adventureConfig.GetHero();
        _location.text = adventureConfig.GetLocation();
    }
}
