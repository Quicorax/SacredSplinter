using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private ServiceElements _servicesElements;

    public static GameManager Instance;


    private void Awake()
    {
        Singletonize();

        if (PlayerPrefs.GetInt("ServicesInitialized") == 0)
            InitializeServiceLocator();
    }

    private void OnDestroy()
    {
        SetServicesUninitialized();
    }
    private void OnApplicationQuit()
    {
        SetServicesUninitialized();
    }
    private void Singletonize()
    {
        if (Instance == null)
            Instance = this;

        DontDestroyOnLoad(this);
    }

    private void InitializeServiceLocator()
    {
        PlayerPrefs.SetInt("ServicesInitialized", 1);

        ServiceFeeder serviceLoader = new();
        serviceLoader.LoadSevices(_servicesElements);
    }

    private void SetServicesUninitialized() => PlayerPrefs.SetInt("ServicesInitialized", 0);
}
