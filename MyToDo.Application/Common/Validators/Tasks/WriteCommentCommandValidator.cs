using FluentValidation;
using MyToDo.Application.CQRS.Tasks.Commands.WriteComment;
using MyToDo.Domain.Errors;

namespace MyToDo.Application.Common.Validators.Tasks;

internal sealed class WriteCommentCommandValidator : AbstractValidator<WriteCommentCommand>
{
    public WriteCommentCommandValidator()
    {
        RuleFor(c => c.TaskId)
            .NotEmpty()
            .WithCustomError(DomainErrors.Task.IdValidationError);

        RuleFor(c => c.MemberId)
            .NotEmpty()
            .WithCustomError(DomainErrors.Task.MemberIdValidationError);

        RuleFor(c => c.Text)
            .NotEmpty()
            .WithCustomError(DomainErrors.Task.CommentTextValidationError);
    }    
}
