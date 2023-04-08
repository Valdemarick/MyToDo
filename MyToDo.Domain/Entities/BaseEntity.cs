namespace MyToDo.Domain.Entities;

/// <summary>
/// Base entity.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Identifier.
    /// </summary>
    public Guid Id { get; set; }
}
