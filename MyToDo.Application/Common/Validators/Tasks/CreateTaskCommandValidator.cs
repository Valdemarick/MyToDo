using FluentValidation;
using MyToDo.Application.CQRS.Tasks.Commands.CreateTask;
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

        RuleFor(c => c.CreatorId)
            .NotEmpty()
            .WithCustomError(DomainErrors.Task.CreatorIdValidationError);

        RuleFor(c => c.TaskType)
            .NotEqual(TaskType.Unknown)
            .WithCustomError(DomainErrors.Task.TypeValidationError);

        RuleFor(c => c.Priority)
            .NotEqual(Priority.Unknown)
            .WithCustomError(DomainErrors.Task.PriorityValidationError);
    }    
}
