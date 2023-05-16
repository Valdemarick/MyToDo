using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Application.Abstractions.Security;
using MyToDo.Domain.Abstractions;
using MyToDo.Domain.Abstractions.Repositories;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Shared;

namespace MyToDo.Application.CQRS.Members.Commands.LoginCommand;

internal sealed class LoginCommandHandler : ICommandHandler<LoginCommand, string>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;

    public LoginCommandHandler(
        IMemberRepository memberRepository, 
        IPasswordHasher passwordHasher, 
        IJwtProvider jwtProvider)
    {
        _memberRepository = memberRepository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByEmail(request.Email, cancellationToken);
        if (member is null)
        {
            return Result.Failure(DomainErrors.Member.MemberNotFound);
        }

        if (!member.IsActive)
        {
            return Result.Failure(DomainErrors.Member.InactiveUserCannotLogin);
        }

        var isPasswordCorrect = _passwordHasher.Verify(member.HashedPassword, request.Password);
        if (!isPasswordCorrect)
        {
            return Result.Failure(DomainErrors.Member.PasswordIsWrong);
        }

        var token = await _jwtProvider.GenerateTokenAsync(member);

        return Result.Success(token);
    }
}
