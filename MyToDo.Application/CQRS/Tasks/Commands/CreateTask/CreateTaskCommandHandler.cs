using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Domain.Abstractions;
using MyToDo.Domain.Abstractions.Repositories;
using MyToDo.Domain.Entities;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Factories;
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
        var creator = await _memberRepository.GetByIdWithoutTrackingAsync(request.CreatorId, cancellationToken);
        if (creator is null)
        {
            return Result.Failure(DomainErrors.Member.MemberNotFound);
        }

        var createTaskCreator = TaskCreatorFactory.Create(creator.FullName, creator.Id);
        if (createTaskCreator.IsFailure)
        {
            return Result.Failure(createTaskCreator.Error);
        }

        TaskExecutor? taskExecutor = null;
        if (request.ExecutorId.HasValue)
        {
            var executor = await _memberRepository.GetByIdWithoutTrackingAsync(request.ExecutorId.Value, cancellationToken);
            if (executor is null)
            {
                return Result.Failure(DomainErrors.Member.MemberNotFound);
            }

            var createTaskExecutor = TaskExecutorFactory.Create(executor.FullName, executor.Id);
            if (createTaskCreator.IsFailure)
            {
                return Result.Failure(createTaskExecutor.Error);
            }

            taskExecutor = createTaskExecutor.Value;
        }

        var createTaskResult = Domain.Factories.TaskFactory.Create(
            request.Title,
            request.Description,
            request.Priority,
            request.TaskType,
            request.Deadline,
            createTaskCreator.Value,
            taskExecutor);
        if (createTaskResult.IsFailure)
        {
            return Result.Failure(createTaskResult.Error);
        }

        _taskRepository.Add(createTaskResult.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
