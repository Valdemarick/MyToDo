﻿using Microsoft.AspNetCore.Authorization;

namespace MyToDo.Infrastructure.Security;

internal class PermissionRequirement : IAuthorizationRequirement
{
    public PermissionRequirement(string permission) => Permission = permission;
    
    public string Permission { get; }
}
