using StandardApi.Domain;
using System.Threading.Tasks;

namespace StandardApi.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password);
    }
}
