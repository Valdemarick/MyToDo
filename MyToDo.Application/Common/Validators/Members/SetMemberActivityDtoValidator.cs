using FluentValidation;
using MyToDo.Application.Common.Dtos.Members;
using MyToDo.Domain.Errors;

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
