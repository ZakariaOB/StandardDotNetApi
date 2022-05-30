using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StandardApi.Authorization
{
    public class HaveMessagePrivilegeHandler : AuthorizationHandler<HaveMessagePriviligeRequest>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HaveMessagePriviligeRequest requirement)
        {
            var userEmailAddress = context.User?.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
            bool containsDigit = userEmailAddress.Any(char.IsDigit);
            if (userEmailAddress.EndsWith(requirement.DomaineName) && !containsDigit)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            context.Fail();
            return Task.CompletedTask;
        }
    }
}
