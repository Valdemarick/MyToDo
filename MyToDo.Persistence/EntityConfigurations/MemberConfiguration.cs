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

        builder.HasMany(m => m.Tasks)
            .WithOne(t => t.Creator)
            .HasForeignKey(t => t.CreatorId);

        builder.HasMany(m => m.Tasks)
            .WithOne(t => t.Executor)
            .HasForeignKey(t => t.ExecutorId);
    }
}
