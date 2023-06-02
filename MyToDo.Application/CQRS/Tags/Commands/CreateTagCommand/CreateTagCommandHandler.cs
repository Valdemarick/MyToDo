using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Domain.Abstractions;
using MyToDo.Domain.Abstractions.Factories;
using MyToDo.Domain.Abstractions.Repositories;
using MyToDo.Domain.Entities;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Shared;

namespace MyToDo.Application.CQRS.Tags.Commands.CreateTagCommand;

internal sealed class CreateTagCommandHandler : ICommandHandler<CreateTagCommand>
{
    private readonly ITagRepository _tagRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITagFactory _tagFactory;

    public CreateTagCommandHandler(
        ITagRepository tagRepository, 
        IUnitOfWork unitOfWork,
        ITagFactory tagFactory)
    {
        _tagRepository = tagRepository;
        _unitOfWork = unitOfWork;
        _tagFactory = tagFactory;
    }

    public async Task<Result> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var tag = await _tagRepository.GetByNameAsync(request.Name, cancellationToken);
        if (tag is not null)
        {
            return Result.Failure(DomainErrors.Tag.TagNameIsAlreadyOccupied);
        }

        var createNewTagResult = _tagFactory.Create(request.Name);
        if (createNewTagResult.IsFailure)
        {
            return Result.Failure(createNewTagResult.Error);
        }

        _tagRepository.Add(createNewTagResult.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
