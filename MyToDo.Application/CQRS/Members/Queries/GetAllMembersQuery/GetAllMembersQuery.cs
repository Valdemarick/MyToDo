using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Application.Common.Dtos.Members;

namespace MyToDo.Application.CQRS.Members.Queries.GetAllMembersQuery;

public record GetAllMembersQuery() : IQuery<List<MemberDto>>;
