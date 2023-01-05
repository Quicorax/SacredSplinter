public class GameConfigPopUp : BaseConfigPopUp
{
    public void Exit()
    {
        ServiceLocator.GetService<NavigationService>().NavigateToScene("00_Menu");
    }
}
