using Domain.Enums;

namespace CinemaAPI;

using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

public class RoleRequirement : IAuthorizationRequirement
{
    public UserRole RequiredRole { get; }

    public RoleRequirement(UserRole requiredRole)
    {
        RequiredRole = requiredRole;
    }
}

public class RoleRequirementHandler : AuthorizationHandler<RoleRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
    {
        // Check if the user is authenticated
        if (context.User.Identity?.IsAuthenticated != true)
        {
            return Task.CompletedTask;
        }

        // Get the role claim from the token
        var roleClaim = context.User.FindFirst("Role")?.Value;
        if (roleClaim == null)
        {
            return Task.CompletedTask;
        }

        // Compare the role with the required role
        if (Enum.TryParse<UserRole>(roleClaim, out var userRole) && userRole == requirement.RequiredRole)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
