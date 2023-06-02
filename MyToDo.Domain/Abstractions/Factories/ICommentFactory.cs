using MyToDo.Domain.Entities;
using MyToDo.Domain.Shared;

namespace MyToDo.Domain.Abstractions.Factories;

public interface ICommentFactory : IBaseFactory
{
    Result<Comment> Create(string? text, Guid taskId, Guid writerId);
}
