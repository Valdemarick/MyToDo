using FluentValidation;
using MyToDo.Application.CQRS.Tags.Commands.DeleteTagCommand;
using MyToDo.Domain.Errors;

namespace MyToDo.Application.Common.Validators.Tags;

internal sealed class DeleteTagCommandValidator : AbstractValidator<DeleteTagCommand>
{
    public DeleteTagCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .WithCustomError(DomainErrors.Tag.IdValidationError);
    }    
}
