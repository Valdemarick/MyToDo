using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Domain.Abstractions;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Shared;

namespace MyToDo.Application.CQRS.Tasks.Commands.AssignTaskCommand;

internal sealed class AssignTaskCommandHandler : ICommandHandler<AssignTaskCommand>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMemberRepository _memberRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeOffsetProvider _dateTimeOffsetProvider;

    public AssignTaskCommandHandler(
        ITaskRepository taskRepository, 
        IMemberRepository memberRepository,
        IUnitOfWork unitOfWork, 
        IDateTimeOffsetProvider dateTimeOffsetProvider)
    {
        _taskRepository = taskRepository;
        _memberRepository = memberRepository;
        _unitOfWork = unitOfWork;
        _dateTimeOffsetProvider = dateTimeOffsetProvider;
    }

    public async Task<Result> Handle(AssignTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id, cancellationToken, isTracking: true);
        if (task is null)
        {
            return Result.Failure(DomainErrors.Task.TaskNotFound);
        }

        var executor = await _memberRepository.GetByIdAsync(request.ExecutorId, cancellationToken);
        if (executor is null)
        {
            return Result.Failure(DomainErrors.Member.MemberNotFound);
        }
        
        task.Assign(executor, _dateTimeOffsetProvider);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
