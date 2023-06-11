﻿using System.Runtime.InteropServices.JavaScript;
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

        public static readonly Error TaskNotFound = new Error(
            "Task.TaskNotFound",
            "There is no such task");

        public static readonly Error TaskStatusValidationError = new(
            "Task. TypeValidationError",
            "Status Validation Error");

        public static readonly Error TaskPriorityValidationError = new(
            "Task. PriorityValidationError",
            "Priority Validation Error");

        public static readonly Error TaskTypeValidationError = new(
            "Task. TypeValidationError",
            "Task Type Validation Error");

        public static readonly Error TitleValidationError = new(
            "Task. TitleValidationError",
            "Task Title Validation Error");

        public static readonly Error DescriptionValidationError = new(
            "Task. DescriptionValidationError",
            "Task Description Validation Error");

        public static readonly Error TaskAlreadyHaveThisTag = new(
            "Task. Tag duplication",
            "Task already have this tag");

        public static readonly Error TaskDoesNotContainThisTag = new(
            "Task. Attempt to remove not linked tag",
            "Task does not have this tag");

        public static readonly Error TagIdValidationError = new(
            "Task. TagId validation error",
            "TagId validation error");

        public static readonly Error IdValidationError = new(
            "Task. Id validation error",
            "Id validation error");

        public static readonly Error MemberIdValidationError = new(
            "Task. MemberId validation error",
            "MemberId validation error");

        public static readonly Error DeadlineValidationError = new(
            "Task. Deadline validation error",
            "Deadline validation error");

        public static readonly Error TitleIsAlreadyOccupied = new(
            "Task. Title is already occupied",
            "Title is already occupied");

        public static readonly Error TaskIsAlreadyClosed = new(
            "Task. Task is already closed",
            "Task is already closed");
    }
    
    public static class Member
    {
        public static readonly Error MemberNotFound = new Error(
            "Member. MemberNotFound",
            "There is not such member");

        public static readonly Error EmailIsAlreadyOccupied = new(
            "Member. Email is already occupied",
            "This email is already occupied");

        public static readonly Error PasswordIsWrong = new(
            "Member. Password is wrong",
            "Password is wrong");

        public static readonly Error FirstNameValidationError = new(
            "Member. FirstNameValidationError",
            "First Name is invalid");

        public static readonly Error LastNameValidationError = new(
            "Member. LastNameValidationError",
            "Last Name is invalid");

        public static readonly Error EmailValidationError = new(
            "Member. EmailValidationError",
            "Email is invalid");

        public static readonly Error PasswordValidationError = new(
            "Member. PasswordValidationError",
            "Password is invalid");

        public static readonly Error RoleValidationError = new(
            "Member. Role validation error",
            "Role validation error");

        public static readonly Error InactiveUserCannotLogin = new(
            "Member. Inactive user cannot login",
            "Inactive user cannot login");

        public static readonly Error IdValidationError = new(
            "Member. Id validation error",
            "Id is invalid");

        public static readonly Error MemberAlreadyHasTheSameActivity = new(
            "Member. Member already has the same activity",
            "Attempt to update activity with the same value");
    }

    public static class Tag
    {
        public static readonly Error TagNameValidationError = new(
            "Tag. NameValidationError",
            "Name is invalid");

        public static readonly Error TagNameIsAlreadyOccupied = new(
            "Tag. Tag name is occupied",
            "The tag with such name already exists");

        public static readonly Error TagNotFound = new(
            "Tag. Tag not found",
            "Tag not found");

        public static readonly Error IdValidationError = new(
            "Tag. Id validation error",
            "Id validation error");
    }

    public static class Role
    {
        public static readonly Error RoleNotFound = new(
            "Role. Role not found",
            "Role not found");
    }

    public static readonly Error FailedToDeserializeObject = new(
        "Deserialization error",
        "Failed to deserialize object");

    public static readonly Error FailedToParseId = new(
        "Parsing error",
        "Failed to parse id");

    public static readonly Error Forbidden = new(
        "Forbidden",
        "You are not allowed to do this");
}
