using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Application.Abstractions.Security;
using MyToDo.Domain.Abstractions;
using MyToDo.Domain.Entities;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Factories;
using MyToDo.Domain.Shared;

namespace MyToDo.Application.CQRS.Members.Commands.RegisterCommand;

internal sealed class RegisterCommandHandler : ICommandHandler<RegisterCommand>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCommandHandler(
        IMemberRepository memberRepository, 
        IPasswordHasher passwordHasher, 
        IUnitOfWork unitOfWork)
    {
        _memberRepository = memberRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingMember = await _memberRepository.GetByEmail(request.Email, cancellationToken);
        if (existingMember is not null)
        {
            return Result.Failure(DomainErrors.Member.EmailIsAlreadyOccupied);
        }
        
        var hashedPassword = _passwordHasher.Hash(request.Password);

        var createMemberResult = MemberFactory.Create(
            request.FirstName,
            request.LastName,
            request.Email,
            hashedPassword);
        if (createMemberResult.IsFailure)
        {
            return Result.Failure(createMemberResult.Error);
        }

        _memberRepository.Add(createMemberResult.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
