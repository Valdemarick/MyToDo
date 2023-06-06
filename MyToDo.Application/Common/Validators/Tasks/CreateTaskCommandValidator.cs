using FluentValidation;
using MyToDo.Application.CQRS.Tasks.Commands.CreateTaskCommand;
using MyToDo.Domain.Enums;
using MyToDo.Domain.Errors;

namespace MyToDo.Application.Common.Validators.Tasks;

internal sealed class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskCommandValidator()
    {
        RuleFor(c => c.Title)
            .NotEmpty()
            .WithCustomError(DomainErrors.Task.TitleValidationError);

        RuleFor(c => c.Description)
            .NotEmpty()
            .WithCustomError(DomainErrors.Task.DescriptionValidationError);

        RuleFor(c => c.Deadline)
            .NotEqual(default(DateTime))
            .WithCustomError(DomainErrors.Task.DeadlineValidationError);
    }    
}
