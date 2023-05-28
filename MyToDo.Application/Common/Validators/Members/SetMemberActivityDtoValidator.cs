using FluentValidation;
using MyToDo.Domain.Errors;
using MyToDo.HttpContracts.Members;

namespace MyToDo.Application.Common.Validators.Members;

internal sealed class SetMemberActivityDtoValidator : AbstractValidator<UpdateMemberActivityDto>
{
    public SetMemberActivityDtoValidator()
    {
        RuleFor(x => x.MemberId)
            .NotEmpty()
            .WithCustomError(DomainErrors.Member.IdValidationError);
    }    
}
