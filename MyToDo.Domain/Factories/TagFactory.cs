using MyToDo.Domain.Entities;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Shared;

namespace MyToDo.Domain.Factories;

public static class TagFactory
{
    public static Result<Tag> Create(string? name)
    {
        if (name is null)
        {
            return Result.Failure(DomainErrors.Tag.TagNameValidationError);
        }

        var tag = Tag.Create(name);

        return Result.Success(tag);
    }
}
