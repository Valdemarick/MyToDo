using FluentValidation;
using MyToDo.Application.CQRS.Tasks.Commands.StartWorkingOnTaskCommand;
using MyToDo.Domain.Errors;

namespace MyToDo.Application.Common.Validators.Tasks;

internal sealed class StartWorkingOnTaskCommandValidator : AbstractValidator<StartWorkingOnTaskCommand>
{
    public StartWorkingOnTaskCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .WithCustomError(DomainErrors.Task.IdValidationError);
    }    
}
