using MyToDo.Domain.Errors;
using MyToDo.Domain.Primitives;
using MyToDo.Domain.Shared;

namespace MyToDo.Domain.Entities;

public sealed class Member : AggregateRoot
{
    private readonly List<Task> _createdTasks = new();
    private readonly List<Task> _assignedTasks = new();

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
        
        Role = role;
    }

    private Member()
    {
    }

    public string FirstName { get; private set; } = string.Empty;
    
    public string LastName { get; private set; } = string.Empty;
    
    public string Email { get; private set; } = string.Empty;

    public string HashedPassword { get; private set; } = string.Empty;
    
    public bool IsActive { get; private set; }

    public string FullName => $"{LastName} {FirstName}";
    
    public DateTimeOffset RegisteredOn { get; private set; }
    
    public Guid RoleId { get; private set; }
    public Role Role { get; private set; } = null!;
    
    public IReadOnlyCollection<Task> CreatedTasks => _createdTasks;
    
    public IEnumerable<Task> AssignedTasks => _assignedTasks;

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
