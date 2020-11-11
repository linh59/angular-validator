using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using SSC.Core.Arguments.Requirements;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SSC.Core.Arguments.Providers
{
    public class PermissionPolicyProvider : IAuthorizationPolicyProvider
    {
        public PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        }

        private DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }


        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => FallbackPolicyProvider.GetDefaultPolicyAsync();
        public Task<AuthorizationPolicy> GetFallbackPolicyAsync() => FallbackPolicyProvider.GetFallbackPolicyAsync();

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith(General.PermissionsPrefix, StringComparison.OrdinalIgnoreCase))
            {
                var policy = new AuthorizationPolicyBuilder();
                policy.AddRequirements(new PermissionRequirement(policyName));
                return Task.FromResult(policy.Build());
            }

            // Policy is not for permissions, try the default provider.
            return FallbackPolicyProvider.GetPolicyAsync(policyName);
        }
    }
}
