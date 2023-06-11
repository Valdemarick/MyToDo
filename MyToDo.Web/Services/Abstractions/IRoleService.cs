using MyToDo.Domain.Shared;
using MyToDo.HttpContracts.Roles;

namespace MyToDo.Web.Services.Abstractions;

internal interface IRoleService
{
    Task<Result<List<RoleDto>>> GetAllAsync(CancellationToken cancellationToken = default);
}
