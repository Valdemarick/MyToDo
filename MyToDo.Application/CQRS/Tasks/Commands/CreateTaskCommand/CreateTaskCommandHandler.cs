using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Domain.Abstractions;
using MyToDo.Domain.Abstractions.Factories;
using MyToDo.Domain.Abstractions.Repositories;
using MyToDo.Domain.Entities;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Factories;
using MyToDo.Domain.Shared;
using MyToDo.Domain.ValueObjects;

namespace MyToDo.Application.CQRS.Tasks.Commands.CreateTaskCommand;

internal sealed class CreateTaskCommandHandler : ICommandHandler<CreateTaskCommand>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMemberRepository _memberRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITaskFactory _taskFactory;

    public CreateTaskCommandHandler(
        ITaskRepository taskRepository, 
        IMemberRepository memberRepository, 
        IUnitOfWork unitOfWork,
        ITaskFactory taskFactory)
    {
        _taskRepository = taskRepository;
        _memberRepository = memberRepository;
        _unitOfWork = unitOfWork;
        _taskFactory = taskFactory;
    }

    public async Task<Result> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var creator = await _memberRepository.GetByIdWithoutTrackingAsync(request.CreatorId, cancellationToken);
        if (creator is null)
        {
            return Result.Failure(DomainErrors.Member.MemberNotFound);
        }

        Member? executor = null;
        if (request.ExecutorId.HasValue)
        {
            executor = await _memberRepository.GetByIdWithoutTrackingAsync(request.ExecutorId.Value, cancellationToken);
            if (executor is null)
            {
                return Result.Failure(DomainErrors.Member.MemberNotFound);
            }
        }

        var createTaskResult = _taskFactory.Create(
            request.Title,
            request.Description,
            request.Priority,
            request.TaskType,
            request.Deadline,
            creator,
            executor);
        if (createTaskResult.IsFailure)
        {
            return Result.Failure(createTaskResult.Error);
        }

        _taskRepository.Add(createTaskResult.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
