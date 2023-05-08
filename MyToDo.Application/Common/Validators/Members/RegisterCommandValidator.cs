using FluentValidation;
using MyToDo.Application.CQRS.Members.Commands.RegisterCommand;
using MyToDo.Domain.Errors;

namespace MyToDo.Application.Common.Validators.Members;

internal sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(c => c.FirstName)
            .NotEmpty()
            .WithCustomError(DomainErrors.Member.FirstNameValidationError);

        RuleFor(c => c.LastName)
            .NotEmpty()
            .WithCustomError(DomainErrors.Member.LastNameValidationError);

        RuleFor(c => c.Email)
            .EmailAddress()
            .WithCustomError(DomainErrors.Member.EmailValidationError);

        RuleFor(c => c.Password)
            .NotEmpty()
            .WithCustomError(DomainErrors.Member.PasswordValidationError);

        RuleFor(c => c.Password.Length)
            .GreaterThanOrEqualTo(8)
            .WithCustomError(DomainErrors.Member.PasswordValidationError);

        RuleFor(c => c.RoleId)
            .NotEmpty()
            .WithCustomError(DomainErrors.Member.RoleIdValidationError);
    }    
}
