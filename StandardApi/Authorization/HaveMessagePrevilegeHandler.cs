using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StandardApi.Authorization
{
    public class HaveMessagePrevilegeHandler : AuthorizationHandler<HaveMessagePrevilegeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HaveMessagePrevilegeRequirement requirement)
        {
            var userEmailAddress = context.User?.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
            if (userEmailAddress.EndsWith(requirement.DomaineName))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            context.Fail();
            return Task.CompletedTask;
        }
    }
}
