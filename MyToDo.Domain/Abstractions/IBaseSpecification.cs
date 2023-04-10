using System.Linq.Expressions;
using MyToDo.Domain.Primitives;

namespace MyToDo.Domain.Abstractions;

public interface IBaseSpecification<TEntity> where TEntity : BaseEntity
{
    Expression<Func<TEntity, bool>>? CriteriaExpression { get; }

    List<Expression<Func<TEntity, object>>> IncludeExpressions { get; }
    
    Expression<Func<TEntity, object>>? OrderByExpression { get; }
    
    Expression<Func<TEntity, object>>? OrderByDescendingExpression { get; }
    
    bool IsTracking { get; }
}
