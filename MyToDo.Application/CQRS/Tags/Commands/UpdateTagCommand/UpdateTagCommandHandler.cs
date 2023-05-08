using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Domain.Abstractions;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Shared;

namespace MyToDo.Application.CQRS.Tags.Commands.UpdateTagCommand;

internal sealed class UpdateTagCommandHandler : ICommandHandler<UpdateTagCommand>
{
    private readonly ITagRepository _tagRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTagCommandHandler(
        ITagRepository tagRepository, 
        IUnitOfWork unitOfWork)
    {
        _tagRepository = tagRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        var tag = await _tagRepository.GetByIdAsync(request.Id, isTracking: true, cancellationToken);
        if (tag is null)
        {
            return Result.Failure(DomainErrors.Tag.TagNotFound);
        }

        var tagWithSuchName = await _tagRepository.GetByNameAsync(request.Name, cancellationToken);
        if (tagWithSuchName is not null)
        {
            return Result.Failure(DomainErrors.Tag.TagNameIsAlreadyOccupied);
        }

        tag.UpdateName(request.Name);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}