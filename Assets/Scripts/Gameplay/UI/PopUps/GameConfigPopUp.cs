public class GameConfigPopUp : BaseConfigPopUp
{
    public void Exit()
    {
        CloseSelf();
        ServiceLocator.GetService<NavigationService>().NavigateToScene("00_Menu");
    }
}
