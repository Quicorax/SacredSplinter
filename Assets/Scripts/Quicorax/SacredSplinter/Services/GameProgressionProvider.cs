using System.Threading.Tasks;
using UnityEngine;

namespace Quicorax.SacredSplinter.Services
{
    public class GameProgressionProvider : IGameProgressionProvider
    {
        private LocalGameProgressionProvider _local = new();
        private RemoteGameProgressionProvider _remote = new();

        public async Task<bool> Initialize()
        {
            await Task.WhenAll(_local.Initialize(), _remote.Initialize());
            return true;
        }
        
        public string Load()
        {
            var localData = _local.Load();
            var remoteData = _remote.Load();

            if (string.IsNullOrEmpty(localData) && string.IsNullOrEmpty(remoteData))
                return null;

            if (string.IsNullOrEmpty(localData) && !string.IsNullOrEmpty(remoteData))
                return remoteData;

            if (!string.IsNullOrEmpty(localData) && string.IsNullOrEmpty(remoteData))
                return localData;

            return CheckConflictingData(localData, remoteData);
        }

        private string CheckConflictingData(string localData, string remoteData)
        {
            var localObject = JsonUtility.FromJson<TicksDeSerializator>(localData);
            var remoteObject = JsonUtility.FromJson<TicksDeSerializator>(remoteData);

            if (remoteObject._ticksPlayed > localObject._ticksPlayed)
                return remoteData;

            return localData;
        }

        public void Save(string text)
        {
            _local.Save(text);
            _remote.Save(text);
        }
    }
}