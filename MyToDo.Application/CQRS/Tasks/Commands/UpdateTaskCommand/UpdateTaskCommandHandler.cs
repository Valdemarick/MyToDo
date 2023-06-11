using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Domain.Abstractions;
using MyToDo.Domain.Abstractions.Factories;
using MyToDo.Domain.Abstractions.Repositories;
using MyToDo.Domain.Entities;
using MyToDo.Domain.Enums;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Shared;
using MyToDo.Domain.ValueObjects;

namespace MyToDo.Application.CQRS.Tasks.Commands.UpdateTaskCommand;

internal sealed class UpdateTaskCommandHandler : ICommandHandler<UpdateTaskCommand>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMemberRepository _memberRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTaskCommandHandler(
        ITaskRepository taskRepository,
        IMemberRepository memberRepository,
        IUnitOfWork unitOfWork)
    {
        _taskRepository = taskRepository;
        _memberRepository = memberRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var taskWithTheSameTitle = await _taskRepository.GetByTitleAsync(request.Title, cancellationToken);
        if (taskWithTheSameTitle is not null && taskWithTheSameTitle.Id != request.Id)
        {
            return Result.Failure(DomainErrors.Task.TitleIsAlreadyOccupied);
        }
        
        var task = await _taskRepository.GetByIdWithoutIncludesAsync(request.Id, cancellationToken, true);
        if (task is null)
        {
            return Result.Failure(DomainErrors.Task.TaskNotFound);
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

        task.Update(request.Title, request.Description, executor, (TaskType)request.TaskType, (Priority)request.Priority, request.Deadline);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
