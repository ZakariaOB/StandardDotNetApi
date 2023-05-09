using Microsoft.AspNetCore.Authorization;

namespace StandardApi.Authorization
{
    public class HaveMessagePrevilegeRequirement : IAuthorizationRequirement
    {
        public string DomaineName { get; }

        public HaveMessagePrevilegeRequirement(string domainName)
        {
            DomaineName = domainName;
        }
    }
}
