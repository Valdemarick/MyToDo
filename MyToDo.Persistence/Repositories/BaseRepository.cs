using Microsoft.EntityFrameworkCore;
using MyToDo.Domain.Abstractions;
using MyToDo.Domain.Abstractions.Repositories;
using MyToDo.Domain.Primitives;
using MyToDo.Persistence.Specifications;

namespace MyToDo.Persistence.Repositories;

internal abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : AggregateRoot
{
    protected readonly ApplicationDbContext DbContext;

    protected readonly DbSet<TEntity> DbSet;

    protected BaseRepository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;

        DbSet = DbContext.Set<TEntity>();
    }

    public void Add(TEntity entity)
    {
        DbContext.Set<TEntity>().Add(entity);
    }

    public void Delete(TEntity entity)
    {
        DbContext.Set<TEntity>().Remove(entity);
    }
    
    public IQueryable<TEntity> ApplySpecification(IBaseSpecification<TEntity> baseSpecification)
    {
        return SpecificationEvaluator.GetQuery(
            DbContext.Set<TEntity>(),
            baseSpecification);
    }
}
