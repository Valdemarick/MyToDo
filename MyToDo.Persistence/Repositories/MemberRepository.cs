﻿using Microsoft.EntityFrameworkCore;
using MyToDo.Domain.Abstractions;
using MyToDo.Domain.Abstractions.Repositories;
using MyToDo.Domain.Entities;
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

    public async Task<Member?> GetByIdAsync(Guid memberId, CancellationToken cancellationToken = default)
    {
        return await SpecificationEvaluator.GetQuery(
                DbContext.Set<Member>(),
                new MemberByIdSpecification(memberId))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Member?> GetByEmail(string email, CancellationToken cancellationToken = default)
    {
        return await SpecificationEvaluator.GetQuery(
                DbContext.Set<Member>(),
                new MemberByEmailSpecification(email))
            .FirstOrDefaultAsync(cancellationToken);
    }
}
