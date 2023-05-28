using MyToDo.Application.Abstractions.Messaging;
using MyToDo.HttpContracts.Members;

namespace MyToDo.Application.CQRS.Members.Queries.GetAllMembersQuery;

public record GetAllMembersQuery() : IQuery<List<MemberDto>>;
