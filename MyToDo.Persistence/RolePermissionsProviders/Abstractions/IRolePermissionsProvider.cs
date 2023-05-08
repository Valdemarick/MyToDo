using MyToDo.Domain.Entities;

namespace MyToDo.Persistence.RolePermissionsProviders.Abstractions;

internal interface IRolePermissionsProvider
{
    List<Permission> GetPermissions();
}