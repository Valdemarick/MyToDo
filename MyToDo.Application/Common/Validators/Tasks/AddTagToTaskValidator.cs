using FluentValidation;
using MyToDo.Application.CQRS.Tasks.Commands.AddTagToTaskCommand;
using MyToDo.Domain.Errors;

namespace MyToDo.Application.Common.Validators.Tasks;

internal sealed class AddTagToTaskValidator : AbstractValidator<AddTagToTaskCommand>
{
    public AddTagToTaskValidator()
    {
        RuleFor(c => c.TaskId)
            .NotEmpty()
            .WithCustomError(DomainErrors.Task.IdValidationError);
        
        RuleFor(c => c.TagId)
            .NotEmpty()
            .WithCustomError(DomainErrors.Task.TagIdValidationError);
    }    
}
