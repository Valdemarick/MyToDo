using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyToDo.Domain.Entities;
using MyToDo.Persistence.Constants;

namespace MyToDo.Persistence.EntityConfigurations;

internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable(TableNames.Permission);
        
        builder.HasKey(p => p.Id);
        builder.HasIndex(p => p.Id).IsUnique();

        builder.Property(p => p.Name).IsRequired();
        builder.HasIndex(p => p.Name).IsUnique();

        builder.HasMany(p => p.Roles)
            .WithMany()
            .UsingEntity<RolePermission>();

        builder.HasData(GetPermissions());
    }

    private static IEnumerable<Permission> GetPermissions()
    {
        return Enum.GetValues<Domain.Enums.Permission>()
            .Select(p => new Permission(p.ToString()));
    }
}
