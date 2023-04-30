using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyToDo.Domain.Entities;
using Task = MyToDo.Domain.Entities.Task;

namespace MyToDo.Persistence.EntityConfigurations;

internal sealed class TaskConfiguration : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder.HasKey(t => t.Id);
        builder.HasIndex(t => t.Id).IsUnique();

        builder.Property(t => t.CreatedOn).IsRequired();

        builder.Property(t => t.Title).IsRequired();
        builder.HasIndex(t => t.Title).IsUnique();

        builder.Property(t => t.Description).IsRequired();

        builder.Property(t => t.CreatorId).IsRequired();

        builder.Property(t => t.Priority).IsRequired();

        builder.Property(t => t.Status).IsRequired();

        builder.Property(t => t.TaskType).IsRequired();

        builder.HasOne<TaskExecutor>(t => t.Executor)
            .WithOne(m => m.Task);

        builder.HasOne<TaskCreator>(t => t.Creator)
            .WithOne(m => m.Task);

        builder.HasMany(t => t.Tags)
            .WithMany(t => t.Tasks);

        builder.HasMany(t => t.Comments)
            .WithOne(c => c.Task)
            .HasForeignKey(c => c.TaskId);
    }
}
