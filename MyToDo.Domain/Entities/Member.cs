using MyToDo.Domain.Errors;
using MyToDo.Domain.Primitives;
using MyToDo.Domain.Shared;
using MyToDo.Domain.ValueObjects;

namespace MyToDo.Domain.Entities;

public sealed class Member : AggregateRoot
{
    internal Member(
        string firstName,
        string lastName,
        string email,
        string hashedPassword,
        bool isActive,
        Role role) : base(Guid.NewGuid())
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        HashedPassword = hashedPassword;
        IsActive = isActive;
        
        RoleId = role.Id;
    }

    private Member()
    {
    }

    public string FirstName { get; private set; }
    
    public string LastName { get; private set; }
    
    public string Email { get; private set; }

    public string HashedPassword { get; private set; }
    
    public bool IsActive { get; private set; }

    public string FullName => $"{LastName} {FirstName}";
    
    public DateTimeOffset RegisteredOn { get; private set; }
    
    public Guid RoleId { get; private set; }
    public Role Role { get; private set; }
    
    public IEnumerable<Task> CreatedTasks { get; set; }
    
    public IEnumerable<Task> AssignedTasks { get; set; }

    public void SetRegisteredOn(DateTimeOffset dateTimeOffset)
    {
        RegisteredOn = dateTimeOffset;
    }

    public Result UpdateActivity(bool isActive)
    {
        if (IsActive == isActive)
        {
            return Result.Failure(DomainErrors.Member.MemberAlreadyHasTheSameActivity);
        }

        IsActive = isActive;

        return Result.Success();
    }
}
