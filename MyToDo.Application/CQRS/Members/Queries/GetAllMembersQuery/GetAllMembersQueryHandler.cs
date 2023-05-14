using AutoMapper;
using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Application.Common.Dtos.Members;
using MyToDo.Domain.Abstractions.Repositories;
using MyToDo.Domain.Shared;

namespace MyToDo.Application.CQRS.Members.Queries.GetAllMembersQuery;

internal sealed class GetAllMembersQueryHandler : IQueryHandler<GetAllMembersQuery, List<MemberDto>>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IMapper _mapper;

    public GetAllMembersQueryHandler(IMemberRepository memberRepository, IMapper mapper)
    {
        _memberRepository = memberRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<MemberDto>>> Handle(GetAllMembersQuery request, CancellationToken cancellationToken)
    {
        var members = await _memberRepository.GetAllAsync(cancellationToken);

        var dtos = _mapper.Map<List<MemberDto>>(members);

        return Result.Success(dtos);
    }
}