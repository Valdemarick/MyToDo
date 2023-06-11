using MyToDo.Domain.Abstractions.Factories;
using MyToDo.Domain.Entities;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Shared;

namespace MyToDo.Domain.Factories;

public sealed class TagFactory : ITagFactory
{
    public Result<Tag> Create(string name)
    {
        if (name is null)
        {
            return Result.Failure(DomainErrors.Tag.TagNameValidationError);
        }

        var tag = new Tag(name);

        return Result.Success(tag);
    }
}
