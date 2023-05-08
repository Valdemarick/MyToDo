using FluentValidation;
using MyToDo.Application.CQRS.Members.Commands.LoginCommand;
using MyToDo.Domain.Errors;

namespace MyToDo.Application.Common.Validators.Members;

internal sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(c => c.Email)
            .EmailAddress()
            .WithCustomError(DomainErrors.Member.EmailValidationError);

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithCustomError(DomainErrors.Member.PasswordValidationError);

        RuleFor(x => x.Password.Length)
            .GreaterThanOrEqualTo(8)
            .WithCustomError(DomainErrors.Member.PasswordValidationError);
    }
}
