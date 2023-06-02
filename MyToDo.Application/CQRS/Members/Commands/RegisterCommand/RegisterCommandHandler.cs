using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Application.Abstractions.Security;
using MyToDo.Domain.Abstractions;
using MyToDo.Domain.Abstractions.Factories;
using MyToDo.Domain.Abstractions.Repositories;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Factories;
using MyToDo.Domain.Shared;

namespace MyToDo.Application.CQRS.Members.Commands.RegisterCommand;

internal sealed class RegisterCommandHandler : ICommandHandler<RegisterCommand>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRoleRepository _roleRepository;
    private readonly IMemberFactory _memberFactory;

    public RegisterCommandHandler(
        IMemberRepository memberRepository, 
        IPasswordHasher passwordHasher, 
        IUnitOfWork unitOfWork, 
        IRoleRepository roleRepository,
        IMemberFactory memberFactory)
    {
        _memberRepository = memberRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
        _roleRepository = roleRepository;
        _memberFactory = memberFactory;
    }

    public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingMember = await _memberRepository.GetByEmail(request.Email, cancellationToken);
        if (existingMember is not null)
        {
            return Result.Failure(DomainErrors.Member.EmailIsAlreadyOccupied);
        }

        var role = await _roleRepository.GetByIdAsync(request.RoleId, cancellationToken);
        if (role is null)
        {
            return Result.Failure(DomainErrors.Role.RoleNotFound);
        }
        
        var hashedPassword = _passwordHasher.Hash(request.Password);

        var createMemberResult = _memberFactory.Create(
            request.FirstName,
            request.LastName,
            request.Email,
            hashedPassword,
            request.IsActive,
            role);
        if (createMemberResult.IsFailure)
        {
            return Result.Failure(createMemberResult.Error);
        }

        _memberRepository.Add(createMemberResult.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
