using Microsoft.EntityFrameworkCore;
using MyToDo.Domain.Entities;
using Task = MyToDo.Domain.Entities.Task;

namespace MyToDo.Persistence;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Task> Tasks => Set<Task>();

    public DbSet<Member> Members => Set<Member>();
}
