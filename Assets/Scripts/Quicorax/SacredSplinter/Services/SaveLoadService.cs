using System.IO;
using UnityEngine;

namespace Quicorax.SacredSplinter.Services
{
    public class SaveLoadService : IService
    {
        private static readonly string KeySavePath = Application.persistentDataPath + "/_gameProgression.json";

        private GameProgressionService _progression;
        private GameConfigService _config;

        public void Initialize(GameConfigService config, GameProgressionService gameProgression)
        {
            _progression = gameProgression;
            _config = config;
            Load();
        }

        public void Save() => File.WriteAllText(KeySavePath, JsonUtility.ToJson(_progression));

        private void Load()
        {
            var data = File.Exists(KeySavePath) ? File.ReadAllText(KeySavePath) : string.Empty;

            if (string.IsNullOrEmpty(data))
                _progression.LoadInitialResources(_config);
            else
                JsonUtility.FromJsonOverwrite(data, _progression);
        }

        public void DeleteLocalFiles()
        {
            if (File.Exists(KeySavePath))
                File.Delete(KeySavePath);
        }
    }
}