﻿using MyToDo.Domain.Entities;
using MyToDo.Domain.Errors;
using MyToDo.Domain.Shared;

namespace MyToDo.Domain.Factories;

public static class MemberFactory
{
    public static Result<Member> Create(
        string? firstName,
        string? lastName,
        string? email,
        string? hashedPassword)
    {
        if (firstName is null)
        {
            return Result.Failure(DomainErrors.Member.FirstNameValidationError);
        }

        if (lastName is null)
        {
            return Result.Failure(DomainErrors.Member.LastNameValidationError);
        }

        if (email is null)
        {
            return Result.Failure(DomainErrors.Member.EmailValidationError);
        }

        if (hashedPassword is null)
        {
            return Result.Failure(DomainErrors.Member.PasswordValidationError);
        }

        var member = Member.Create(
            firstName,
            lastName,
            email,
            hashedPassword);

        return Result.Success(member);
    }
}
