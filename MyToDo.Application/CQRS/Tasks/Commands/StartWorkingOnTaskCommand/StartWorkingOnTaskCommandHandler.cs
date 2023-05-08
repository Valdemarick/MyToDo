using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Domain.Abstractions;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Shared;

namespace MyToDo.Application.CQRS.Tasks.Commands.StartWorkingOnTaskCommand;

internal sealed class StartWorkingOnTaskCommandHandler : ICommandHandler<StartWorkingOnTaskCommand>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeOffsetProvider _dateTimeOffsetProvider;

    public StartWorkingOnTaskCommandHandler(ITaskRepository taskRepository, IUnitOfWork unitOfWork, IDateTimeOffsetProvider dateTimeOffsetProvider)
    {
        _taskRepository = taskRepository;
        _unitOfWork = unitOfWork;
        _dateTimeOffsetProvider = dateTimeOffsetProvider;
    }

    public async Task<Result> Handle(StartWorkingOnTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id, cancellationToken);
        if (task is null)
        {
            return Result.Failure(DomainErrors.Task.TaskNotFound);
        }

        task.StartWorkingOnTask(_dateTimeOffsetProvider);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}
