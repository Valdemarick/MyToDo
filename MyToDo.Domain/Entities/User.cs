using Microsoft.AspNetCore.Identity;

namespace MyToDo.Domain.Entities;

/// <summary>
/// User entity.
/// </summary>
public sealed class User : IdentityUser<Guid>
{
    /// <summary>
    /// Task list, assigned to the user.
    /// </summary>
    public ICollection<Task> Tasks { get; set; } = null!;
}
