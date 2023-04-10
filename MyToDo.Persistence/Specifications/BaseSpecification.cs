using System.Linq.Expressions;
using MyToDo.Domain.Primitives;

namespace MyToDo.Persistence.Specifications;

public abstract class BaseSpecification<TEntity> where TEntity : BaseEntity
{
    protected BaseSpecification(Expression<Func<TEntity, bool>> criteria) =>
        Criteria = criteria;

    public Expression<Func<TEntity, bool>>? Criteria { get; }

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
