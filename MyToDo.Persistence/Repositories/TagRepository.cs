using Microsoft.EntityFrameworkCore;
using MyToDo.Domain.Abstractions;
using MyToDo.Domain.Abstractions.Repositories;
using MyToDo.Domain.Entities;

namespace MyToDo.Persistence.Repositories;

internal sealed class TagRepository : BaseRepository<Tag>, ITagRepository
{
    public TagRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Tag>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Tag>()
            .ToListAsync(cancellationToken);
    }

    public async Task<Tag?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Tag>()
            .FirstOrDefaultAsync(t => t.Name.ToLower() == name.ToLower(), cancellationToken);
    }

    public async Task<Tag?> GetByIdAsync(Guid id, bool isTracking = false, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Tag>()
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }
}
