using System.Linq.Expressions;
using MyToDo.Domain.Abstractions;
using MyToDo.Domain.Primitives;

namespace MyToDo.Persistence.Specifications;

internal abstract class BaseSpecification<TEntity> : IBaseSpecification<TEntity>
    where TEntity : BaseEntity
{
    protected BaseSpecification(
        Expression<Func<TEntity, bool>> criteriaExpression,
        bool isTracking = false)
    {
        CriteriaExpression = criteriaExpression;
        IsTracking = isTracking;
    }

    public Expression<Func<TEntity, bool>>? CriteriaExpression { get; }

    public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = new();
    
    public Expression<Func<TEntity, object>>? OrderByExpression { get; private set; }
    
    public Expression<Func<TEntity, object>>? OrderByDescendingExpression { get; private set; }

    public bool IsTracking { get; protected set; } 

    protected void AddInclude(Expression<Func<TEntity, object>> includeExpression) => 
        IncludeExpressions.Add(includeExpression);

    protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression) =>
        OrderByExpression = orderByExpression;

    protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescExpression) =>
        OrderByDescendingExpression = orderByDescExpression;
}
