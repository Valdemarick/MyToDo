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
        return await DbContext.Set<Member>()
            .ToListAsync(cancellationToken);
    }

    public async Task<Member?> GetByIdWithoutTrackingAsync(Guid memberId, CancellationToken cancellationToken = default)
    {
        return await SpecificationEvaluator.GetQuery(
                DbContext.Set<Member>(),
                new MemberByIdWithoutTrackingSpecification(memberId))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Member?> GetByEmail(string email, CancellationToken cancellationToken = default)
    {
        return await SpecificationEvaluator.GetQuery(
                DbContext.Set<Member>(),
                new MemberByEmailSpecification(email))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Member?> GetByIdWithTrackingAsync(Guid memberId, CancellationToken cancellationToken = default)
    {
        return await SpecificationEvaluator.GetQuery(
                DbContext.Set<Member>(),
                new MemberByIdWithTrackingSpecification(memberId))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<MemberPagedList> GetMemberPageAsync(MemberPageRequest request, CancellationToken cancellationToken = default)
    {
        IQueryable<Member> query = DbContext.Members;

        if (!string.IsNullOrWhiteSpace(request.SearchString))
        {
            query = query.Where(m => m.FirstName.ToLower().StartsWith(request.SearchString.ToLower()) || 
                                     m.LastName.ToLower().StartsWith(request.SearchString.ToLower()));
        }

        var totalCount = DbContext.Members.Count();

        var members = await query
            .Skip(((request.PageIndex - 1) * request.PageSize))
            .Take(request.PageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return new MemberPagedList
        {
            Items = members,
            TotalCount = totalCount
        };
    }
}
