using Microsoft.AspNetCore.Authorization;
using MyToDo.Infrastructure.Constants;

namespace MyToDo.Infrastructure.Security;

internal sealed class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        PermissionRequirement requirement)
    {
        var permission = context
            .User
            .Claims
            .Where(c => c.Type == CustomClaims.Permissions)
            .Select(x => x.Value)
            .ToList();

        if (permission.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }

        return Task.CompletedTask;
    }
}
