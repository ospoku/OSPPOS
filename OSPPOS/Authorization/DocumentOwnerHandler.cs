using DMX.Data;
using DMX.Models;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace DMX.Authorization
{
    public class DocumentOwnerHandler : AuthorizationHandler<DocumentOwnerRequirement, Letter>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DocumentOwnerRequirement requirement, Letter resource)
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
