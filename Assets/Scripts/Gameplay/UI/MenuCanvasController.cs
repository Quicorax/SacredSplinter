using UnityEngine;
public class MenuCanvasController : MonoBehaviour
{
    private PopUpSpawnerService _popUpSpawner;

    [SerializeField]
    private PopUpLauncher _adventureSelector, _quests, _shop, _encyclopedia;

    private void Start()
    {
        _popUpSpawner = ServiceLocator.GetService<PopUpSpawnerService>();
    }

    public void OpenAdventureSelector() =>
        _popUpSpawner.SpawnPopUp(_adventureSelector);
    public void OpenQuests() => 
        _popUpSpawner.SpawnPopUp(_quests);
    public void OpenShop() => 
        _popUpSpawner.SpawnPopUp(_shop);
    public void OpenEncyclopedia() =>
        _popUpSpawner.SpawnPopUp(_encyclopedia);
}
