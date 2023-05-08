using MyToDo.Domain.Primitives;

namespace MyToDo.Domain.Entities;

public sealed class Member : AggregateRoot
{
    private Member(
        string firstName,
        string lastName,
        string email,
        string hashedPassword,
        Role role) : base(Guid.NewGuid())
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        HashedPassword = hashedPassword;

        RoleId = role.Id;
    }

    private Member()
    {
    }

    public string FirstName { get; private set; }
    
    public string LastName { get; private set; }
    
    public string Email { get; private set; }

    public string HashedPassword { get; private set; }

    public string FullName => $"{LastName} {FirstName}";
    
    public DateTimeOffset RegisteredOn { get; private set; }
    
    public Guid RoleId { get; private set; }
    public Role Role { get; private set; }

    public void SetRegisteredOn(DateTimeOffset dateTimeOffset)
    {
        RegisteredOn = dateTimeOffset;
    }

    internal static Member Create(
        string firstName,
        string lastName,
        string email,
        string hashedPassword,
        Role role)
    {
        return new Member(
            firstName,
            lastName,
            email,
            hashedPassword,
            role);
    }
}
