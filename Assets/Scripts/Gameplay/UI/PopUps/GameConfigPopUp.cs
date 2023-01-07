public class GameConfigPopUp : BaseConfigPopUp
{
    private void Start()
    {
        SetSound(ServiceLocator.GetService<GameProgressionService>());
    }

    public void Exit()
    {
        CloseSelf();
        ServiceLocator.GetService<NavigationService>().NavigateToScene("00_Menu");
    }
}
