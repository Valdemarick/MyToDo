using Microsoft.EntityFrameworkCore;
using MyToDo.Domain.Abstractions;
using MyToDo.Domain.Entities;
using MyToDo.Persistence.Specifications;
using MyToDo.Persistence.Specifications.MemberSpecifications;

namespace MyToDo.Persistence.Repositories;

public sealed class MemberRepository : BaseRepository<Member>, IMemberRepository
{
    public MemberRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Member?> GetByIdAsync(Guid memberId, CancellationToken cancellationToken = default)
    {
        return await SpecificationEvaluator.GetQuery(
                DbContext.Set<Member>(),
                new MemberByIdSpecification(memberId))
            .FirstOrDefaultAsync(cancellationToken);
    }
}
