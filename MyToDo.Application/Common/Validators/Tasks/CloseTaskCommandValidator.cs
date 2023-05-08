using FluentValidation;
using MyToDo.Application.CQRS.Tasks.Commands.CloseTaskCommand;
using MyToDo.Domain.Errors;

namespace MyToDo.Application.Common.Validators.Tasks;

internal sealed class CloseTaskCommandValidator : AbstractValidator<CloseTaskCommand>
{
    public CloseTaskCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .WithCustomError(DomainErrors.Task.IdValidationError);
    }
}
