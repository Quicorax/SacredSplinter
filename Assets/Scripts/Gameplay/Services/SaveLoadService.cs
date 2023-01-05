using System.IO;
using UnityEngine;

public class SaveLoadService : IService
{
    private static string _kSavePath = Application.persistentDataPath + "/_gameProgression.json";

    private GameProgressionService _gameProgression;
    private GameConfigService _config;

    public void Initialize(GameConfigService config, GameProgressionService gameProgression)
    {
        Debug.Log(_kSavePath);

        _gameProgression = gameProgression;
        _config = config;
        Load();
    }

    public void Save() => File.WriteAllText(_kSavePath, JsonUtility.ToJson(_gameProgression));

    private void Load()
    {
        string data = File.Exists(_kSavePath) ? File.ReadAllText(_kSavePath) : string.Empty;

        if (string.IsNullOrEmpty(data))
            _gameProgression.LoadInitialResources(_config);
        else
            JsonUtility.FromJsonOverwrite(data, _gameProgression);
    }
    public void DeleteLocalFiles()
    {
        if (File.Exists(_kSavePath))
            File.Delete(_kSavePath);
    }
}
