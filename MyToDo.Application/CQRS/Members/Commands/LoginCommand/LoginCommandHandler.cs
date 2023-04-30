using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Application.Abstractions.Security;
using MyToDo.Domain.Abstractions;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Shared;

namespace MyToDo.Application.CQRS.Members.Commands.LoginCommand;

internal sealed class LoginCommandHandler : ICommandHandler<LoginCommand, string>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IPasswordHasher _passwordHasher;

    public LoginCommandHandler(
        IMemberRepository memberRepository, 
        IPasswordHasher passwordHasher)
    {
        _memberRepository = memberRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByEmail(request.Email, cancellationToken);
        if (member is null)
        {
            return Result.Failure(DomainErrors.Member.MemberNotFound);
        }

        var isPasswordCorrect = _passwordHasher.Verify(member.HashedPassword, request.Password);
        
        return !isPasswordCorrect ? Result.Failure(DomainErrors.Member.PasswordIsWrong) : Result.Success();
    }
}
