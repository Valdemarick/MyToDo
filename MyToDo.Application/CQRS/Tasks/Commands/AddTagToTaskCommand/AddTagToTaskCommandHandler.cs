using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Domain.Abstractions;
using MyToDo.Domain.Abstractions.Repositories;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Shared;

namespace MyToDo.Application.CQRS.Tasks.Commands.AddTagToTaskCommand;

internal sealed class AddTagToTaskCommandHandler : ICommandHandler<AddTagToTaskCommand>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ITagRepository _tagRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddTagToTaskCommandHandler(ITaskRepository taskRepository, ITagRepository tagRepository, IUnitOfWork unitOfWork)
    {
        _taskRepository = taskRepository;
        _tagRepository = tagRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(AddTagToTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetWithCommentsAsync(request.TaskId, cancellationToken);
        if (task is null)
        {
            return Result.Failure(DomainErrors.Task.TaskNotFound);
        }

        var tag = await _tagRepository.GetByIdAsync(request.TagId, cancellationToken: cancellationToken);
        if (tag is null)
        {
            return Result.Failure(DomainErrors.Tag.TagNotFound);
        }

        var addTagResult = task.AddTag(tag);
        if (addTagResult.IsFailure)
        {
            return Result.Failure(addTagResult.Error);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
