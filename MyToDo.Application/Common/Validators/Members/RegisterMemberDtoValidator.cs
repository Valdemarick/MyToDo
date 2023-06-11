using FluentValidation;
using MyToDo.Domain.Errors;
using MyToDo.HttpContracts.Members;

namespace MyToDo.Application.Common.Validators.Members;

internal sealed class RegisterMemberDtoValidator : AbstractValidator<RegisterMemberDto>
{
    public RegisterMemberDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithCustomError(DomainErrors.Member.FirstNameValidationError);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithCustomError(DomainErrors.Member.LastNameValidationError);

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithCustomError(DomainErrors.Member.EmailValidationError);
    }    
}
