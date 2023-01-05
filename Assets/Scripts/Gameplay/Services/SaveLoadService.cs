using System.IO;
using UnityEngine;

public class SaveLoadService : IService
{
    private static string _kSavePath = Application.persistentDataPath + "/_gameProgression.json";

    private GameProgressionService _progression;
    private GameConfigService _initialConfig;

    public void Initialize(GameConfigService config, GameProgressionService gameProgression)
    {
        Debug.Log(_kSavePath);

        _progression = gameProgression;
        _initialConfig = config;
        Load();
    }

    public void Save() => File.WriteAllText(_kSavePath, JsonUtility.ToJson(_progression));

    private void Load()
    {
        string data = File.Exists(_kSavePath) ? File.ReadAllText(_kSavePath) : string.Empty;

        if (string.IsNullOrEmpty(data))
            _progression.LoadInitialResources(_initialConfig);
        else
            JsonUtility.FromJsonOverwrite(data, _progression);
    }
    public void DeleteLocalFiles()
    {
        if (File.Exists(_kSavePath))
            File.Delete(_kSavePath);
    }
}
