using Microsoft.EntityFrameworkCore;
using MyToDo.Domain.Abstractions;
using MyToDo.Domain.Primitives;

namespace MyToDo.Persistence.Specifications;

internal static class SpecificationEvaluator
{
    public static IQueryable<TEntity> GetQuery<TEntity>(
        IQueryable<TEntity> source,
        IBaseSpecification<TEntity>? specification) where TEntity : BaseEntity
    {
        var query = source;

        if (specification is null)
        {
            return query;
        }

        if (specification.CriteriaExpression is not null)
        {
            query = query.Where(specification.CriteriaExpression);
        }

        query = specification.IncludeExpressions.Aggregate(
            query,
            (current, includeExpression) => current.Include(includeExpression));

        if (specification.OrderByExpression is not null)
        {
            query = query.OrderBy(specification.OrderByExpression);
        }
        else if (specification.OrderByDescendingExpression is not null)
        {
            query = query.OrderByDescending(specification.OrderByDescendingExpression);
        }

        if (!specification.IsTracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }
}
