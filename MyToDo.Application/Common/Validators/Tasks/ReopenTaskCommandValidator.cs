using FluentValidation;
using MyToDo.Application.CQRS.Tasks.Commands.ReopenTaskCommand;
using MyToDo.Domain.Errors;

namespace MyToDo.Application.Common.Validators.Tasks;

internal sealed class ReopenTaskCommandValidator : AbstractValidator<ReopenTaskCommand>
{
    public ReopenTaskCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .WithCustomError(DomainErrors.Task.IdValidationError);
    }
}
