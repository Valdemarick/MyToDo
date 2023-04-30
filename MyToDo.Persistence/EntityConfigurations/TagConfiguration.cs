using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyToDo.Domain.Entities;

namespace MyToDo.Persistence.EntityConfigurations;

internal sealed class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasKey(t => t.Id);
        builder.HasIndex(t => t.Id).IsUnique();
        
        builder.Property(t => t.Name).IsRequired();
        builder.HasIndex(t => t.Name).IsUnique();

        builder.Property(t => t.CreatedOn).IsRequired();

        builder.HasMany(t => t.Tasks)
            .WithMany(t => t.Tags);
    }
}
