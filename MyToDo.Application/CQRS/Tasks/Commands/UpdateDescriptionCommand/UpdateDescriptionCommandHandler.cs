using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Domain.Abstractions;
using MyToDo.Domain.Abstractions.Repositories;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Shared;

namespace MyToDo.Application.CQRS.Tasks.Commands.UpdateDescriptionCommand;

internal sealed class UpdateDescriptionCommandHandler : ICommandHandler<UpdateDescriptionCommand>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDescriptionCommandHandler(
        ITaskRepository taskRepository,
        IUnitOfWork unitOfWork)
    {
        _taskRepository = taskRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateDescriptionCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.TaskId, cancellationToken, isTracking: true);
        if (task is null)
        {
            return Result.Failure(DomainErrors.Task.TaskNotFound);
        }

        task.UpdateDescription(request.Description);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
