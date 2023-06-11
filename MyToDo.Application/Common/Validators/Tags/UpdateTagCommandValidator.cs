using FluentValidation;
using MyToDo.Application.CQRS.Tags.Commands.UpdateTagCommand;
using MyToDo.Domain.Errors;

namespace MyToDo.Application.Common.Validators.Tags;

internal sealed class UpdateTagCommandValidator : AbstractValidator<UpdateTagCommand>
{
    public UpdateTagCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .WithCustomError(DomainErrors.Tag.IdValidationError);
        
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithCustomError(DomainErrors.Tag.TagNameValidationError);
    }
}
