using System.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.Core.Environments;

namespace Quicorax.SacredSplinter.Services
{
    public interface IServicesInitializer
    {
        Task Initialize(string environmentId);
    }
    
    public class ServicesInitializer : IServicesInitializer
    {
        public async Task Initialize(string environmentId)
        {
            var options = new InitializationOptions();
            if (!string.IsNullOrEmpty(environmentId))
            {
                options.SetEnvironmentName(environmentId);
            }

            await UnityServices.InitializeAsync(options);
        }
    }
}