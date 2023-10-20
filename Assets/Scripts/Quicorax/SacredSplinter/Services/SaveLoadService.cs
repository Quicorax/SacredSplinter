using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Quicorax.SacredSplinter.Services
{
    public interface ISaveLoadService
    {
        void Initialize();
        void Save() ;
        void DeleteLocalFiles();
    }
    
    public class SaveLoadService : ISaveLoadService
    {
        private static readonly string SavePathKey = Application.persistentDataPath + "/gameProgression.json";

        [Inject] private IGameProgressionService _progression;
        [Inject] private IGameConfigService _config;
        [Inject] private IGameProgressionProvider _progressionProvider;
        
        private bool _saving;
        private readonly int _saveBufferDelayMS = 200;
        
        public void Initialize()
        {
            var data = _progressionProvider.Load();
            if (string.IsNullOrEmpty(data))
                _progression.LoadInitialResources();
            else
            {
                JsonUtility.FromJsonOverwrite(data, _progression);
                _progression.DeserializeModels();
            }
        }

        public void Save() => SaveBuffer();
           
        public void DeleteLocalFiles()
        {
            if (File.Exists(SavePathKey))
            {
                File.Delete(SavePathKey);
            }
        }
        
        private async void SaveBuffer()
        {
            if (_saving)
                return;         
            
            _saving = true;

            await Task.Delay(_saveBufferDelayMS);
            
            _progression.SerializeModels(() => _progressionProvider.Save(JsonUtility.ToJson(_progression)));
            _saving = false;
        }
    }
}