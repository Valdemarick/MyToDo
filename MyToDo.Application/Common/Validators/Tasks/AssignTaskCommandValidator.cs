using FluentValidation;
using MyToDo.Application.CQRS.Tasks.Commands.AssignTaskCommand;
using MyToDo.Domain.Errors;

namespace MyToDo.Application.Common.Validators.Tasks;

internal sealed class AssignTaskCommandValidator : AbstractValidator<AssignTaskCommand>
{
    public AssignTaskCommandValidator()
    {
        RuleFor(c => c.TaskId)
            .NotEmpty()
            .WithCustomError(DomainErrors.Task.IdValidationError);

        RuleFor(c => c.MemberId)
            .NotEmpty()
            .WithCustomError(DomainErrors.Task.MemberIdValidationError);
    }    
}
