using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Application.Common.Dtos.Members;

namespace MyToDo.Application.CQRS.Members.Queries.GetMemberPageQuery;

public record GetMemberPageQuery(MemberPageRequestDto Parameters) : IQuery<MemberPagedListDto>;
