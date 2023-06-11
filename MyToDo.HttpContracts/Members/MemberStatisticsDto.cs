namespace MyToDo.HttpContracts.Members;

public sealed class MemberStatisticsDto
{
    public Guid MemberId { get; set; }

    public int TasksToDoCount { get; set; }

    public int TasksInProgressCount { get; set; }

    public int ClosedTasksCount { get; set; }
    
    public int CreatedTasksCount { get; set; }

    public double AverageTimeOfClosingTaskInHours { get; set; }
}
