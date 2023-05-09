using Microsoft.EntityFrameworkCore;
using MyToDo.Domain.Entities;
using MyToDo.Infrastructure.Services.Abstractions;
using MyToDo.Persistence;

namespace MyToDo.Infrastructure.Providers;

internal sealed class PermissionService : IPermissionService
{
    private readonly ApplicationDbContext _dbContext;

    public PermissionService(ApplicationDbContext dbContext)
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
