using MyToDo.Domain.Primitives;

namespace MyToDo.Domain.Abstractions.Repositories;

public interface IBaseRepository<TEntity>
    where TEntity : AggregateRoot
{
    IQueryable<TEntity> ApplySpecification(IBaseSpecification<TEntity> baseSpecification);

    void Add(TEntity entity);

    void Delete(TEntity entity);
}
