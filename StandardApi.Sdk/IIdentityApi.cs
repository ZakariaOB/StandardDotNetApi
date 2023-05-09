using Refit;
using StandardApi.Contracts.V1.Requests;
using StandardApi.Contracts.V1.Responses;
using System.Threading.Tasks;

namespace StandardApi.Sdk
{
    public interface IIdentityApi
    {
        [Post("/api/v1/identity/register")]
        Task<ApiResponse<AuthSuccessResponse>> RegisterAsync([Body]UserRegistrationRequest request);

        [Post("/api/v1/identity/login")]
        Task<ApiResponse<AuthSuccessResponse>> LoginAsync([Body] UserLoginRequest request);

        [Post("/api/v1/identity/refresh")]
        Task<ApiResponse<AuthSuccessResponse>> RefreshAsync([Body] RefreshTokenRequest request);
    }
}
