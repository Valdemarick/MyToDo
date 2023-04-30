﻿using MyToDo.Domain.Entities;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Shared;

namespace MyToDo.Domain.Factories;

public static class CommentFactory
{
    public static Result<Comment> Create(string? text, Guid taskId, Guid writerId)
    {
        if (text is null)
        {
            return Result.Failure(DomainErrors.Comment.TextValidationError);
        }

        if (taskId == Guid.Empty)
        {
            return Result.Failure(DomainErrors.Comment.TaskIdValidationError);
        }

        if (writerId == Guid.Empty)
        {
            return Result.Failure(DomainErrors.Comment.WriterIdValidationError);
        }

        var comment = Comment.Create(
            text,
            taskId,
            writerId);

        return Result.Success(comment);
    }
}
