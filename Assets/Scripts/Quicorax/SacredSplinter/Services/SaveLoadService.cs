using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace Quicorax.SacredSplinter.Services
{
    public class SaveLoadService : IService
    {
        private static readonly string SavePathKey = Application.persistentDataPath + "/gameProgression.json";

        private GameProgressionService _progression;
        private GameConfigService _config;
        private IGameProgressionProvider _progressionProvider;
        
        private bool _saving;
        private readonly float _saveBufferDelay = 0.5f;
        
        public void Initialize(GameConfigService config, GameProgressionService progression,
            IGameProgressionProvider progressionProvider)
        {
            _progression = progression;
            _config = config;
            _progressionProvider = progressionProvider;

            Load();
        }

        public void Save() => SaveBuffer();
           
        public void DeleteLocalFiles()
        {
            if (File.Exists(SavePathKey))
                File.Delete(SavePathKey);
        }
        
        private async void SaveBuffer()
        {
            if (_saving)
                return;         
            
            _saving = true;

            await Task.Delay(200);
            
            _progression.SerializeModels(() => _progressionProvider.Save(JsonUtility.ToJson(_progression)));
            _saving = false;
        }
        
        private void Load()
        {
            var data = _progressionProvider.Load();
            if (string.IsNullOrEmpty(data))
                _progression.LoadInitialResources(_config);
            else
            {
                JsonUtility.FromJsonOverwrite(data, _progression);
                _progression.DeserializeModels();
            }
        }


    }
}