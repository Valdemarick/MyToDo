using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Domain.Abstractions;
using MyToDo.Domain.Abstractions.Repositories;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Shared;

namespace MyToDo.Application.CQRS.Tasks.Commands.LinkTagsToTaskCommand;

internal sealed class LinkTagsToTaskCommandHandler : ICommandHandler<LinkTagsToTaskCommand>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ITagRepository _tagRepository;
    private readonly IUnitOfWork _unitOfWork;

    public LinkTagsToTaskCommandHandler(
        ITaskRepository taskRepository, 
        ITagRepository tagRepository, 
        IUnitOfWork unitOfWork)
    {
        _taskRepository = taskRepository;
        _tagRepository = tagRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(LinkTagsToTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.TaskId, cancellationToken, true);
        if (task is null)
        {
            return Result.Failure(DomainErrors.Task.TaskNotFound);
        }

        var tags = await _tagRepository.GetByIdsAsync(request.TagIds, cancellationToken);
        if (tags.Count != request.TagIds.Count)
        {
            return Result.Failure(DomainErrors.Tag.TagNotFound);
        }
        
        task.UnlinkAllTags();
        task.LinkTags(tags);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
