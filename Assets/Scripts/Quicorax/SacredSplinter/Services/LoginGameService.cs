using System.Threading.Tasks;
using Unity.Services.Authentication;

namespace Quicorax.SacredSplinter.Services
{
    public interface ILoginGameService
    {
        Task Initialize();
    }
    
    public class LoginGameService : ILoginGameService
    {
        public async Task Initialize()
        {
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }
        }
    }
}