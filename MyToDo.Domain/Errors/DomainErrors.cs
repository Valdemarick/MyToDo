﻿using MyToDo.Domain.Shared;

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
    }
}
