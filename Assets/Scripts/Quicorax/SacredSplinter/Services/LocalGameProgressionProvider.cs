using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace Quicorax.SacredSplinter.Services
{
    public class LocalGameProgressionProvider : IGameProgressionProvider
    {
        private static readonly string SavePathKey = Application.persistentDataPath + "/gameProgression.json";

        public async Task<bool> Initialize()
        {
            await Task.Yield();
            return true;
        }

        public string Load() => File.Exists(SavePathKey) ? File.ReadAllText(SavePathKey) : string.Empty;
        public void Save(string text) => File.WriteAllText(SavePathKey, text);
    }
}