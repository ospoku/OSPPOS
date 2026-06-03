using DMX.Data;
using DMX.Models;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace OSPPOS.Authorization
{
    public class DeceasedOwnerHandler : AuthorizationHandler<DeceasedOwnerRequirement, Deceased>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DeceasedOwnerRequirement requirement, Deceased resource)
        {
            // Check if the user has a claim of type ClaimTypes.Name with the value of resource.CreatedBy
            if (context.User.HasClaim(x=>x.Type==ClaimTypes.NameIdentifier && x.Value==resource.CreatedBy))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
