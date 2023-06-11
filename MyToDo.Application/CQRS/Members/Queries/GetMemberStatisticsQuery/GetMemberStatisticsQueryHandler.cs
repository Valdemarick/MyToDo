using MyToDo.Application.Abstractions.Messaging;
using MyToDo.Domain.Abstractions.Repositories;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Shared;
using MyToDo.HttpContracts.Members;
using TaskStatus = MyToDo.Domain.Enums.TaskStatus;

namespace MyToDo.Application.CQRS.Members.Queries.GetMemberStatisticsQuery;

internal sealed class GetMemberStatisticsQueryHandler : IQueryHandler<GetMemberStatisticsQuery, MemberStatisticsDto>
{
    private readonly IMemberRepository _memberRepository;

    public GetMemberStatisticsQueryHandler(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<Result<MemberStatisticsDto>> Handle(GetMemberStatisticsQuery request, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetWithTasksAsync(request.MemberId, cancellationToken);
        if (member is null)
        {
            return Result.Failure(DomainErrors.Member.MemberNotFound);
        }

        var createdTasksCount = member.CreatedTasks.Count();
        var tasksToDoCount = member.AssignedTasks.Count(t => t.Status is TaskStatus.Open or TaskStatus.Reopen);
        var tasksInProgressCount = member.AssignedTasks.Count(t => t.Status == TaskStatus.InProgress);
        var completedTasksCount = member.AssignedTasks.Count(t => t.Status == TaskStatus.Completed);

        var closedTasksTimes = member.AssignedTasks
            .Where(t => t.Status == TaskStatus.Completed)
            .Select(t => t.CompletedOn.Value.Subtract(t.CreatedOn))
            .ToList();

        var averageTimeOfClosingTasks =
            closedTasksTimes.Any()
                ? ((closedTasksTimes.Aggregate((a, b) => a + b)).TotalHours) / closedTasksTimes.Count
                : 0.00;

        var dto = new MemberStatisticsDto
        {
            CreatedTasksCount = completedTasksCount,
            TasksInProgressCount = tasksInProgressCount,
            ClosedTasksCount = completedTasksCount,
            TasksToDoCount = tasksToDoCount,
            AverageTimeOfClosingTaskInHours = averageTimeOfClosingTasks
        };

        return Result.Success(dto);
    }
}
