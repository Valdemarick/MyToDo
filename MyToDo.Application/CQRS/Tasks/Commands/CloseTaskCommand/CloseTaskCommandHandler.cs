using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Domain.Abstractions;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Shared;
using Task = MyToDo.Domain.Entities.Task;

namespace MyToDo.Application.CQRS.Tasks.Commands.CloseTaskCommand;

internal sealed class CloseTaskCommandHandler : ICommandHandler<CloseTaskCommand>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CloseTaskCommandHandler(
        ITaskRepository taskRepository, 
        IUnitOfWork unitOfWork)
    {
        _taskRepository = taskRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CloseTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id, cancellationToken, isTracking: true);
        if (task is null)
        {
            return Result.Failure(DomainErrors.Task.TaskNotFound);
        }

        task.Close();
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
