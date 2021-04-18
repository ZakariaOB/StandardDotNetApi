using Microsoft.AspNetCore.Mvc;
using StandardApi.Contracts;
using StandardApi.Contracts.V1.Requests;
using StandardApi.Contracts.V1.Responses;
using StandardApi.Services;
using System.Threading.Tasks;

namespace StandardApi.Controllers.V1
{
    public class IdentityApiController : Controller
    {
        private readonly IIdentityService _identityService;

        public IdentityApiController(IIdentityService identityService)
        {
            this._identityService = identityService;
        }

        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register([FromBody]UserRegistrationRequest request)
        {
            var authResponse = await _identityService.RegisterAsync(request.Email, request.Password);
            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSucessResponse 
            { 
                Token = authResponse.Token
            });
        }
    }
}
