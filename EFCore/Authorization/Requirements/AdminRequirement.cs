using Microsoft.AspNetCore.Authorization;
using Web.Api.Constants;

namespace Web.Api.Authorization.Requirements
{
    public class AdminAuthorizationHandler : AuthorizationHandler<AdminRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirement requirement)
        {
            if (context.User.IsInRole(Roles.SuperAdmin) | context.User.IsInRole(Roles.Admin))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
    public class AdminRequirement : IAuthorizationRequirement
    {

    }
}