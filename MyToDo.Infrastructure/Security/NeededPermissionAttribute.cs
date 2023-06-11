using Microsoft.AspNetCore.Authorization;
using MyToDo.Domain.Enums;

namespace MyToDo.Infrastructure.Security;

public sealed class NeededPermissionAttribute : AuthorizeAttribute
{
    public NeededPermissionAttribute(Permission permission) : base(policy: permission.ToString())
    {
    }
}
