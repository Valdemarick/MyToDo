using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyToDo.Domain.Entities;
using MyToDo.Persistence.Constants;

namespace MyToDo.Persistence.EntityConfigurations;

internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(TableNames.Role);
        
        builder.HasKey(r => r.Id);
        builder.HasIndex(r => r.Id).IsUnique();

        builder.Property(r => r.Name);
        builder.HasIndex(r => r.Name).IsUnique();

        builder.HasMany(r => r.Members)
            .WithOne(m => m.Role)
            .HasForeignKey(m => m.RoleId);

        builder.HasMany(r => r.Permissions)
            .WithMany()
            .UsingEntity<RolePermission>();

        builder.HasData(GetRoles());
    }

    private static IEnumerable<Role> GetRoles()
    {
        return Enum.GetValues<Domain.Enums.Role>()
            .Select(r => new Role(r.ToString()));
    }
}
