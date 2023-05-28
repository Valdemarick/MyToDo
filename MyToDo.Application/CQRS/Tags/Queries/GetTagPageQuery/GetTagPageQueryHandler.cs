using AutoMapper;
using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Domain.Abstractions.Repositories;
using MyToDo.Domain.Shared;
using MyToDo.Domain.ValueObjects.Requests;
using MyToDo.HttpContracts.Tags;

namespace MyToDo.Application.CQRS.Tags.Queries.GetTagPageQuery;

internal sealed class GetTagPageQueryHandler : IQueryHandler<GetTagPageQuery, TagPagedListDto>
{
    private readonly ITagRepository _tagRepository;
    private readonly IMapper _mapper;

    public GetTagPageQueryHandler(ITagRepository tagRepository, IMapper mapper)
    {
        _tagRepository = tagRepository;
        _mapper = mapper;
    }

    public async Task<Result<TagPagedListDto>> Handle(GetTagPageQuery query, CancellationToken cancellationToken)
    {
        var request = new TagPageRequest(query.SearchString, query.PageIndex, query.PageSize);

        var tagPagedList = await _tagRepository.GetPageAsync(request, cancellationToken);

        var tagsDto = _mapper.Map<List<TagDto>>(tagPagedList.Items);

        var tagPagedListDto = new TagPagedListDto(tagsDto, tagPagedList.TotalCount, query.PageIndex,
            query.PageSize);

        return Result.Success(tagPagedListDto);
    }
}
