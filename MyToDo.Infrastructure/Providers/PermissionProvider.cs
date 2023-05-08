using Microsoft.EntityFrameworkCore;
using MyToDo.Domain.Entities;
using MyToDo.Infrastructure.Providers.Abstractions;
using MyToDo.Persistence;

namespace MyToDo.Infrastructure.Providers;

internal sealed class PermissionProvider : IPermissionProvider
{
    private readonly ApplicationDbContext _dbContext;

    public PermissionProvider(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Permission>> GetMemberPermissionsAsync(Guid memberId)
    {
        return await _dbContext.Set<Member>()
            .Include(m => m.Role)
            .ThenInclude(r => r.Permissions)
            .Where(m => m.Id == memberId)
            .Select(m => m.Role)
            .SelectMany(r => r.Permissions)
            .ToListAsync();
    }
}
