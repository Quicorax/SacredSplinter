using System;
using System.Threading.Tasks;
using Unity.Services.RemoteConfig;
using UnityEngine;
using RemoteConfig = Unity.Services.RemoteConfig;

namespace Quicorax.SacredSplinter.Services
{
    public interface IRemoteConfigService
    {
        Task Initialize();
        T GetFromJSON<T>(string key, T defaultValue = default);
    }
    
    public class RemoteConfigService : IRemoteConfigService
    {
        private struct appData { }
        private struct userData { }

        [Serializable]
        private class Wrapper<T> { public T data; }

        private RuntimeConfig _config;

        public async Task Initialize()
        {
            _config = await RemoteConfig.RemoteConfigService.Instance.FetchConfigsAsync(new userData(), new appData());
        }

        public T GetFromJSON<T>(string key, T defaultValue = default)
        {
            var data = _config?.GetJson(key, "{}");

            if (string.IsNullOrEmpty(data))
                return defaultValue;

            try
            {
                return JsonUtility.FromJson<Wrapper<T>>(data).data;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return defaultValue;
            }
        }
    }
}