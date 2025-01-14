using Microsoft.AspNetCore.Authorization;
using Web.Api.Constants;

namespace Web.Api.Authorization.Requirements
{
    public class CustomerAuthorizationHandler : AuthorizationHandler<CustomerRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomerRequirement requirement)
        {
            if (context.User.IsInRole(Roles.SuperAdmin)
                | context.User.IsInRole(Roles.Admin)
                | context.User.IsInRole(Roles.Customer))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
    public class CustomerRequirement : IAuthorizationRequirement
    {

    }
}