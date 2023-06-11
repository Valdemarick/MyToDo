﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyToDo.Domain.Entities;
using MyToDo.Domain.ValueObjects;
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

        builder.Property(m => m.IsActive).IsRequired().HasDefaultValue(true);
        
        builder.HasOne<Role>(m => m.Role)
            .WithMany(r => r.Members)
            .HasForeignKey(m => m.RoleId);

        builder.HasMany(m => m.CreatedTasks)
            .WithOne(t => t.Creator)
            .HasForeignKey(t => t.CreatorId);
        
        builder.HasMany(m => m.AssignedTasks)
            .WithOne(t => t.Executor)
            .HasForeignKey(t => t.ExecutorId);
    }
}
