using System.Threading.Tasks;
using Quicorax.SacredSplinter.Services;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ServiceElements _servicesElements;

    public static GameManager Instance;

    private ServiceFeeder _serviceFeeder;

    private void Awake()
    {
        Singletonize();

        _serviceFeeder = new ServiceFeeder();
        Initialize().ManageTaskException();
    }

    private void Singletonize()
    {
        if (Instance == null)
            Instance = this;

        DontDestroyOnLoad(this);
    }

    private async  Task Initialize()
    {
        await _serviceFeeder.LoadServices(_servicesElements);
        
        ServiceLocator.GetService<NavigationService>().NavigateToMenu();
    }
}