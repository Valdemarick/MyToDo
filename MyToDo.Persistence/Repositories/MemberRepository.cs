using Microsoft.EntityFrameworkCore;
using MyToDo.Domain.Abstractions.Repositories;
using MyToDo.Domain.Entities;
using MyToDo.Domain.ValueObjects.PagedLists;
using MyToDo.Domain.ValueObjects.Requests;
using MyToDo.Persistence.Specifications;
using MyToDo.Persistence.Specifications.MemberSpecifications;

namespace MyToDo.Persistence.Repositories;

internal sealed class MemberRepository : BaseRepository<Member>, IMemberRepository
{
    public MemberRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Member>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet
            .ToListAsync(cancellationToken);
    }

    public async Task<Member?> GetByIdWithoutTrackingAsync(Guid memberId, CancellationToken cancellationToken = default)
    {
        return await SpecificationEvaluator.GetQuery(
                DbSet,
                new MemberByIdWithoutTrackingSpecification(memberId))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Member?> GetByEmail(string email, CancellationToken cancellationToken = default)
    {
        return await SpecificationEvaluator.GetQuery(
                DbSet,
                new MemberByEmailSpecification(email))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Member?> GetByEmailWithRoleAsync(string email, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(x => x.Email == email)
            .Include(x => x.Role)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Member?> GetByIdWithTrackingAsync(Guid memberId, CancellationToken cancellationToken = default)
    {
        return await SpecificationEvaluator.GetQuery(
                DbSet,
                new MemberByIdWithTrackingSpecification(memberId))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<MemberPagedList> GetMemberPageAsync(MemberPageRequest request, CancellationToken cancellationToken = default)
    {
        IQueryable<Member> query = DbSet;

        if (!string.IsNullOrWhiteSpace(request.SearchString))
        {
            query = query.Where(m => m.FirstName.ToLower().StartsWith(request.SearchString.ToLower()) || 
                                     m.LastName.ToLower().StartsWith(request.SearchString.ToLower()));
        }

        var totalCount = DbSet.Count();

        var members = await query
            .Skip(((request.PageIndex - 1) * request.PageSize))
            .Take(request.PageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return new MemberPagedList(members, totalCount);
    }

    public async Task<Member?> GetWithTasksAsync(Guid memberId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(m => m.AssignedTasks)
            .Include(m => m.CreatedTasks)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == memberId, cancellationToken);
    }
}
