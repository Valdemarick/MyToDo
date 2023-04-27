﻿using MyToDo.Application.Abstractions.Messaging;

namespace MyToDo.Application.CQRS.Members.Commands.RegisterCommand;

public sealed record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : ICommand;
    