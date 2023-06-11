using MyToDo.Application.Abstractions.Messaging;
using MyToDo.HttpContracts.Members;

namespace MyToDo.Application.CQRS.Members.Queries.GetMemberStatisticsQuery;

public sealed record GetMemberStatisticsQuery(Guid MemberId) : IQuery<MemberStatisticsDto>;
