using Microsoft.AspNetCore.Authorization;

namespace StandardApi.Authorization
{
    public class HaveMessagePriviligeRequest : IAuthorizationRequirement
    {
        public string DomaineName { get; }

        public HaveMessagePriviligeRequest(string domainName)
        {
            DomaineName = domainName;
        }
    }
}
