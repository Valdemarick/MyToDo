using MyToDo.Domain.Entities;
using MyToDo.Domain.Shared;

namespace MyToDo.Domain.Abstractions.Factories;

public interface ITagFactory : IBaseFactory
{
    Result<Tag> Create(string? name);
}
