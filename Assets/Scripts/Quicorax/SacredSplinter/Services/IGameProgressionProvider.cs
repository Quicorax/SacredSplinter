using System.Threading.Tasks;

namespace Quicorax.SacredSplinter.Services
{
    public interface IGameProgressionProvider
    {
        Task<bool> Initialize();
        string Load();
        void Save(string text);
    }
}