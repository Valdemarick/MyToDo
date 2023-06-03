using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Domain.Abstractions;
using MyToDo.Domain.Abstractions.Repositories;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Shared;

namespace MyToDo.Application.CQRS.Tasks.Commands.RemoveTagFromTaskCommand;

internal sealed class RemoveTagFromTaskCommandHandler : ICommandHandler<RemoveTagFromTaskCommand>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ITagRepository _tagRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveTagFromTaskCommandHandler(ITaskRepository taskRepository, ITagRepository tagRepository, IUnitOfWork unitOfWork)
    {
        _taskRepository = taskRepository;
        _tagRepository = tagRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RemoveTagFromTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.TaskId, cancellationToken, isTracking: true);
        if (task is null)
        {
            return Result.Failure(DomainErrors.Task.TaskNotFound);
        }

        var tag = await _tagRepository.GetByIdAsync(request.TagId, cancellationToken: cancellationToken);
        if (tag is null)
        {
            return Result.Failure(DomainErrors.Tag.TagNotFound);
        }

        var removeTagResult = task.RemoveTag(tag);
        if (removeTagResult.IsFailure)
        {
            return Result.Failure(removeTagResult.Error);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
