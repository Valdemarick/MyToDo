using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyToDo.Domain.Entities;
using MyToDo.Persistence.Constants;

namespace MyToDo.Persistence.EntityConfigurations;

internal sealed class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.ToTable(TableNames.Member);
        
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

        builder.HasOne<Role>(m => m.Role)
            .WithMany(r => r.Members)
            .HasForeignKey(m => m.RoleId);
    }
}
