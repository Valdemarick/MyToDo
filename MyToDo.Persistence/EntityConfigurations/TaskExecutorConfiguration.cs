using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyToDo.Domain.Entities;
using Task = MyToDo.Domain.Entities.Task;

namespace MyToDo.Persistence.EntityConfigurations;

internal sealed class TaskExecutorConfiguration : IEntityTypeConfiguration<TaskExecutor>
{
    public void Configure(EntityTypeBuilder<TaskExecutor> builder)
    {
        builder.HasKey(te => te.Id);
        builder.HasIndex(te => te.Id).IsUnique();

        builder.Property(te => te.FullName);

        builder.HasOne<Task>(te => te.Task)
            .WithOne(t => t.Executor)
            .HasForeignKey<Task>(t => t.ExecutorId);

        builder.HasOne<Member>()
            .WithMany()
            .HasForeignKey(te => te.MemberId);
    }
}
