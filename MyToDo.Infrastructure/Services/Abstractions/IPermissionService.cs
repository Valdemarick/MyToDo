using MyToDo.Domain.Entities;

namespace MyToDo.Infrastructure.Services.Abstractions;

internal interface IPermissionService
{
    Task<IEnumerable<Permission>> GetMemberPermissionsAsync(Guid memberId);
}
