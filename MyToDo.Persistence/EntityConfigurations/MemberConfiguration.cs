using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyToDo.Domain.Entities;

namespace MyToDo.Persistence.EntityConfigurations;

internal sealed class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.HasKey(m => m.Id);
        builder.HasIndex(m => m.Id).IsUnique();

        builder.Property(m => m.FirstName).IsRequired();

        builder.Property(m => m.LastName).IsRequired();

        builder.Property(m => m.Email).IsRequired();
        builder.HasIndex(m => m.Email).IsUnique();

        builder.Property(m => m.HashedPassword).IsRequired();

        builder.Property(m => m.RegisteredOn).IsRequired();

        builder.HasMany<TaskExecutor>()
            .WithOne()
            .HasForeignKey(te => te.MemberId);

        builder.HasMany<TaskCreator>()
            .WithOne()
            .HasForeignKey(tc => tc.MemberId);
    }
}
