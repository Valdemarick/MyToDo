using MyToDo.Application.Abstractions.Messaging;
using MyToDo.HttpContracts.Members;

namespace MyToDo.Application.CQRS.Members.Queries.GetMemberPageQuery;

public record GetMemberPageQuery(
    string? SearchString,
    int PageIndex, 
    int PageSize) : IQuery<MemberPagedListDto>;
