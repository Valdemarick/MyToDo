using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyToDo.Domain.Entities;
using Task = MyToDo.Domain.Entities.Task;

namespace MyToDo.Persistence.EntityConfigurations;

internal sealed class TaskCreatorConfiguration : IEntityTypeConfiguration<TaskCreator>
{
    public void Configure(EntityTypeBuilder<TaskCreator> builder)
    {
        builder.HasKey(tc => tc.Id);
        builder.HasIndex(tc => tc.Id).IsUnique();

        builder.Property(tc => tc.FullName).IsRequired();

        builder.HasOne<Member>()
            .WithMany()
            .HasForeignKey(tc => tc.MemberId);

        builder.HasOne(tc => tc.Task)
            .WithOne(t => t.Creator)
            .HasForeignKey<Task>(t => t.CreatorId);
    }
}
