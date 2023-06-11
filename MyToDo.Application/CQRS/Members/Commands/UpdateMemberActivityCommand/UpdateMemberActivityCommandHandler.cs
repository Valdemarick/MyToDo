using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Domain.Abstractions;
using MyToDo.Domain.Abstractions.Repositories;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Shared;

namespace MyToDo.Application.CQRS.Members.Commands.UpdateMemberActivityCommand;

internal sealed class UpdateMemberActivityCommandHandler : ICommandHandler<UpdateMemberActivityCommand>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMemberActivityCommandHandler(IMemberRepository memberRepository, IUnitOfWork unitOfWork)
    {
        _memberRepository = memberRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateMemberActivityCommand request, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdWithTrackingAsync(request.MemberId, cancellationToken);
        if (member is null)
        {
            return Result.Failure(DomainErrors.Member.MemberNotFound);
        }

        var updateResult = member.UpdateActivity(request.IsActive);
        if (updateResult.IsFailure)
        {
            return Result.Failure(updateResult.Error);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
