using FluentValidation;
using MyToDo.Application.CQRS.Tasks.Commands.RemoveTagFromTaskCommand;
using MyToDo.Domain.Errors;

namespace MyToDo.Application.Common.Validators.Tasks;

internal sealed class RemoveTagFromTaskCommandValidator : AbstractValidator<RemoveTagFromTaskCommand>
{
    public RemoveTagFromTaskCommandValidator()
    {
        RuleFor(c => c.TaskId)
            .NotEmpty()
            .WithCustomError(DomainErrors.Task.IdValidationError);

        RuleFor(c => c.TagId)
            .NotEmpty()
            .WithCustomError(DomainErrors.Task.TagIdValidationError);
    }    
}
