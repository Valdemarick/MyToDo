using MyToDo.Application.Abstractions.Messaging;
using MyToDo.HttpContracts.Members;

namespace MyToDo.Application.CQRS.Members.Queries.GetByIdQuery;

public sealed record GetMemberByIdQuery(Guid Id) : IQuery<MemberDto>;
