using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyToDo.Domain.Entities;
using MyToDo.Domain.Errors;
using MyToDo.Domain.ValueObjects;
using MyToDo.Persistence.Constants;
using Task = MyToDo.Domain.Entities.Task;

namespace MyToDo.Persistence.EntityConfigurations;

internal sealed class TaskConfiguration : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder.ToTable(TableNames.Task);
        
        builder.HasKey(t => t.Id);
        builder.HasIndex(t => t.Id).IsUnique();

        builder.Property(t => t.CreatedOn).IsRequired();

        builder.Property(t => t.Title).IsRequired();
        builder.HasIndex(t => t.Title).IsUnique();

        builder.Property(t => t.Description).IsRequired();

        builder.Property(t => t.Priority).IsRequired();

        builder.Property(t => t.Status).IsRequired();

        builder.Property(t => t.TaskType).IsRequired();

        builder.Property(t => t.DeadLine).IsRequired();

        builder.Property(t => t.CreatorId).IsRequired();

        builder.HasOne(t => t.Creator)
            .WithMany(c => c.CreatedTasks)
            .HasForeignKey(t => t.CreatorId);

        builder.HasOne(t => t.Executor)
            .WithMany(e => e.AssignedTasks)
            .HasForeignKey(t => t.ExecutorId);

        builder.HasMany(t => t.Tags)
            .WithMany(t => t.Tasks);
    }
}
