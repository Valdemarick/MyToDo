using AutoMapper;
using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Domain.Abstractions.Repositories;
using MyToDo.Domain.Shared;
using MyToDo.Domain.ValueObjects.Requests;
using MyToDo.HttpContracts.Members;

namespace MyToDo.Application.CQRS.Members.Queries.GetMemberPageQuery;

public sealed class GetMemberPageQueryHandler : IQueryHandler<GetMemberPageQuery, MemberPagedListDto>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IMapper _mapper;

    public GetMemberPageQueryHandler(IMemberRepository memberRepository, IMapper mapper)
    {
        _memberRepository = memberRepository;
        _mapper = mapper;
    }

    public async Task<Result<MemberPagedListDto>> Handle(GetMemberPageQuery query, CancellationToken cancellationToken)
    {
        var request = new MemberPageRequest(query.SearchString, query.PageIndex, query.PageSize);
        
        var pagedList = await _memberRepository.GetMemberPageAsync(request, cancellationToken);

        var membersDto = _mapper.Map<List<MemberDto>>(pagedList.Items);

        var pagedListDto = new MemberPagedListDto(membersDto, pagedList.TotalCount, query.PageIndex,
            query.PageSize);

        return Result.Success(pagedListDto);
    }
}
