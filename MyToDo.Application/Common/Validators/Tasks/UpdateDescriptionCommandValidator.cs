using FluentValidation;
using MyToDo.Application.CQRS.Tasks.Commands.UpdateDescription;
using MyToDo.Domain.Errors;

namespace MyToDo.Application.Common.Validators.Tasks;

internal sealed class UpdateDescriptionCommandValidator : AbstractValidator<UpdateDescriptionCommand>
{
    public UpdateDescriptionCommandValidator()
    {
        RuleFor(c => c.TaskId)
            .NotEmpty()
            .WithCustomError(DomainErrors.Task.IdValidationError);

        RuleFor(c => c.Description)
            .NotEmpty()
            .WithCustomError(DomainErrors.Task.DescriptionValidationError);
    }    
}
