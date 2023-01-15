using System.IO;
using UnityEngine;

namespace Quicorax.SacredSplinter.Services
{
    public class SaveLoadService : IService
    {
        private static readonly string SavePathKey = Application.persistentDataPath + "/gameProgression.json";

        private GameProgressionService _progression;
        private GameConfigService _config;
        private IGameProgressionProvider _progressionProvider;

        public void Initialize(GameConfigService config, GameProgressionService progression, IGameProgressionProvider progressionProvider)
        {
            _progression = progression;
            _config = config;
            _progressionProvider = progressionProvider;
            
            Load();
        }

        public void Save() => _progressionProvider.Save(JsonUtility.ToJson(_progression));

        private void Load()
        {
            var data = _progressionProvider.Load();
            if (string.IsNullOrEmpty(data))
                _progression.LoadInitialResources(_config);
            else
                JsonUtility.FromJsonOverwrite(data, _progression);
        }

        public void DeleteLocalFiles()
        {
            if (File.Exists(SavePathKey))
                File.Delete(SavePathKey);
        }
    }
}