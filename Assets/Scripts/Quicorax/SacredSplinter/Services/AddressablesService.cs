using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Quicorax.SacredSplinter.Services
{
    public interface IAddressablesService
    {
        Task Initialize(List<Sprite> assets);
        void LoadAddrssComponentObject<T>(string key, Transform parent, Action<T> taskAction);
        Task<T> LoadAddrssAsset<T>(string key);
        void ReleaseAddressable(GameObject addressableInstance);
    }

    public class AddressablesService : IAddressablesService
    {
        public async Task Initialize(List<Sprite> assets)
        {
            foreach (var asset in assets)
            {
                await LoadAddrssAsset<Sprite>(asset.name);
            }
        }

        public void LoadAddrssComponentObject<T>(string key, Transform parent, Action<T> taskAction) =>
            LoadAddrssOfComponentAsync(key, parent, taskAction).ManageTaskException();

        public async Task<T> LoadAddrssAsset<T>(string key) => await Addressables.LoadAssetAsync<T>(key).Task;
        
        public void ReleaseAddressable(GameObject addressableInstance) => Addressables.Release(addressableInstance);

        private async Task LoadAddrssOfComponentAsync<T>(string key, Transform parent, Action<T> taskAction)
        {
            await Addressables.LoadAssetAsync<GameObject>(key).Task;
            var loadedAsset = await Addressables.InstantiateAsync(key, parent).Task;

            taskAction?.Invoke(loadedAsset.GetComponent<T>());
        }
    }
}