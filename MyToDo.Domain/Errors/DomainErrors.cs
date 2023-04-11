using MyToDo.Domain.Shared;

namespace MyToDo.Domain.Errors;

public static class DomainErrors
{
    public static class Task
    {
        public static readonly Error TaskIsAlreadyInProgress = new Error(
            "Task.TaskIsAlreadyInProgress",
            "Can't start working on task that's already in progress");

        public static readonly Error TaskIsNotCompleted = new Error(
            "Task.TaskIsNotCompleted",
            "Can't reopen not completed task");

        public static readonly Error TaskIsCompleted = new Error(
            "Task.TaskIsCompleted",
            "Can't start working on task due to it's completed");

        public static readonly Error TaskIsAlreadyCompleted = new Error(
            "Task.TaskIsAlreadyCompleted",
            "Can't complete task due to it's already completed");

        public static readonly Error TaskDoesNotHaveThisComment = new Error(
            "Task.TaskDoesNotHaveThisComment",
            "Can't remove comment cause the task does not have it");

        public static readonly Error TaskNotFound = new Error(
            "Task.TaskNotFound",
            "There is no such task");
    }
    
    public static class Member
    {
        public static readonly Error MemberNotFound = new Error(
            "Member. MemberNotFound",
            "There is not such member");
    }
        
}
