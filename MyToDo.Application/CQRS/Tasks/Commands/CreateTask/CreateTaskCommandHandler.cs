using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Domain.Abstractions;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Shared;

namespace MyToDo.Application.CQRS.Tasks.Commands.CreateTask;

internal sealed class CreateTaskCommandHandler : ICommandHandler<CreateTaskCommand>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMemberRepository _memberRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTaskCommandHandler(
        ITaskRepository taskRepository, 
        IMemberRepository memberRepository, 
        IUnitOfWork unitOfWork)
    {
        _taskRepository = taskRepository;
        _memberRepository = memberRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdAsync(request.CreatorId, cancellationToken);
        if (member is null)
        {
            return Result.Failure(DomainErrors.Member.MemberNotFound);
        }

        var createTaskResult = Domain.Factories.TaskFactory.Create(
            request.Title,
            request.Description,
            request.Priority,
            request.TaskType,
            request.CreatorId,
            request.ExecutorId);
        if (createTaskResult.IsFailure)
        {
            return Result.Failure(createTaskResult.Error);
        }

        _taskRepository.Add(createTaskResult.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
