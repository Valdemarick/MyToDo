using MyToDo.Domain.Primitives;

namespace MyToDo.Domain.Abstractions;

public interface IRepository<T>
    where T : AggregateRoot
{
}
