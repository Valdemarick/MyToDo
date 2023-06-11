using AutoMapper;
using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Domain.Abstractions.Repositories;
using MyToDo.Domain.Shared;
using MyToDo.HttpContracts.Tags;

namespace MyToDo.Application.CQRS.Tags.Queries.GetAllTagsQuery;

internal sealed class GetAllTagsQueryHandler : IQueryHandler<GetAllTagsQuery, List<TagDto>>
{
    private readonly ITagRepository _tagRepository;
    private readonly IMapper _mapper;

    public GetAllTagsQueryHandler(ITagRepository tagRepository, IMapper mapper)
    {
        _tagRepository = tagRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<TagDto>>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
    {
        var tags = await _tagRepository.GetAllAsync(cancellationToken);

        var dtos = _mapper.Map<List<TagDto>>(tags);

        return Result.Success(dtos);
    }
}
