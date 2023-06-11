using FluentValidation;
using MyToDo.Application.CQRS.Tags.Commands.CreateTagCommand;
using MyToDo.Domain.Errors;

namespace MyToDo.Application.Common.Validators.Tags;

internal sealed class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
{
    public CreateTagCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithCustomError(DomainErrors.Tag.TagNameValidationError);
    }
}
