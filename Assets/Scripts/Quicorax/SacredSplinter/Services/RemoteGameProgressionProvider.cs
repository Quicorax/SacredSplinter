using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.CloudSave;
using UnityEngine;

namespace Quicorax.SacredSplinter.Services
{
    public class RemoteGameProgressionProvider : IGameProgressionProvider
    {
        private string _remoteData = string.Empty;
        private bool _sendingToRemote;

        public async Task<bool> Initialize()
        {
            var savedData = await CloudSaveService.Instance.Data.LoadAsync();
            savedData.TryGetValue("data", out _remoteData);
            return true;
        }

        public string Load() => _remoteData;

        public void Save(string text)
        {
            _remoteData = text;

            if (!_sendingToRemote)
                SendSaveFiles().ManageTaskException();
        }
        
        private async Task SendSaveFiles()
        {
            _sendingToRemote = true;
            await Task.Delay(500);

            try
            {
                await CloudSaveService.Instance.Data.ForceSaveAsync(
                    new Dictionary<string, object> { { "data", _remoteData } });
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

            _sendingToRemote = false;
        }
    }
}