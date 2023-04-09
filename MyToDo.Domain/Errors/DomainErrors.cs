using MyToDo.Domain.Shared;

namespace MyToDo.Domain.Errors;

public static class DomainErrors
{
    public static class Task
    {
        public static readonly Error TaskAlreadyInProgress = new Error(
            "Task.TaskAlreadyInProgress",
            "Can't start working on task that's already in progress");

        public static readonly Error TaskNotCompleted = new Error(
            "Task.TaskNotCompleted",
            "Can't reopen not completed task");
    }
}
