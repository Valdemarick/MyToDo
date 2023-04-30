using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyToDo.Domain.Entities;

namespace MyToDo.Persistence.EntityConfigurations;

internal sealed class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.Id).IsUnique();

        builder.Property(c => c.Text).IsRequired();

        builder.Property(c => c.CreatedOn).IsRequired();

        builder.HasOne(c => c.Task)
            .WithMany(t => t.Comments);

        builder.HasMany<Member>()
            .WithMany();
    }
}
