using MyToDo.Domain.Entities;

namespace MyToDo.Infrastructure.Providers.Abstractions;

internal interface IPermissionProvider
{
    Task<IEnumerable<Permission>> GetMemberPermissionsAsync(Guid memberId);
}
