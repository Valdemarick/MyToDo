﻿namespace MyToDo.HttpContracts.Members;

public sealed class MemberDto
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }
    
    public string LastName { get; set; }

    public string Email { get; set; }

    public bool IsActive { get; set; }
}
