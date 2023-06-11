﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyToDo.Persistence;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MyToDo.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230506132154_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CommentMember", b =>
                {
                    b.Property<Guid>("CommentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("MemberId")
                        .HasColumnType("uuid");

                    b.HasKey("CommentId", "MemberId");

                    b.HasIndex("MemberId");

                    b.ToTable("CommentMember");
                });

            modelBuilder.Entity("MyToDo.Domain.Entities.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("LastUpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("TaskId")
                        .HasColumnType("uuid");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("WriterId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("TaskId");

                    b.ToTable("Comments", (string)null);
                });

            modelBuilder.Entity("MyToDo.Domain.Entities.Member", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("RegisteredOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.ToTable("Members", (string)null);
                });

            modelBuilder.Entity("MyToDo.Domain.Entities.Permission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Permissions", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("25cbfeb0-5009-4a9e-bbf1-3ab7776a64d4"),
                            Name = "TaskRead"
                        },
                        new
                        {
                            Id = new Guid("c39bb671-4cbb-4324-9dd5-2c6ea9cb0a41"),
                            Name = "TaskManagement"
                        },
                        new
                        {
                            Id = new Guid("93cfef56-f157-4ff2-84d2-32ae0ff38abf"),
                            Name = "CommentLeaving"
                        },
                        new
                        {
                            Id = new Guid("3b627c14-9078-403e-baaa-4f481904bafa"),
                            Name = "UserManagement"
                        },
                        new
                        {
                            Id = new Guid("61ff1cff-0f9e-4320-90a9-620399172afa"),
                            Name = "UserRead"
                        },
                        new
                        {
                            Id = new Guid("2c51f088-d02c-4532-9fbe-3075ff8b77e2"),
                            Name = "TagRead"
                        },
                        new
                        {
                            Id = new Guid("a6a4b4ee-820d-4e48-9099-3c51233ecc5a"),
                            Name = "TagManagement"
                        });
                });

            modelBuilder.Entity("MyToDo.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("31265c07-cbae-4009-a7e5-50fac2b71c43"),
                            Name = "Admin"
                        },
                        new
                        {
                            Id = new Guid("8326d391-66a0-4213-a774-e505ea8457dd"),
                            Name = "Contributor"
                        },
                        new
                        {
                            Id = new Guid("6cf54ee0-a83c-4c1e-8ccc-9b4a2eae75c7"),
                            Name = "User"
                        });
                });

            modelBuilder.Entity("MyToDo.Domain.Entities.RolePermission", b =>
                {
                    b.Property<Guid>("PermissionId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("PermissionId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePermission");
                });

            modelBuilder.Entity("MyToDo.Domain.Entities.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("LastUpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Tags", (string)null);
                });

            modelBuilder.Entity("MyToDo.Domain.Entities.Task", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset?>("CompletedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("ExecutorId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset?>("LastUpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Priority")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("TaskType")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId")
                        .IsUnique();

                    b.HasIndex("ExecutorId")
                        .IsUnique();

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("Tasks", (string)null);
                });

            modelBuilder.Entity("MyToDo.Domain.Entities.TaskCreator", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("MemberId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("MemberId");

                    b.ToTable("TaskCreators", (string)null);
                });

            modelBuilder.Entity("MyToDo.Domain.Entities.TaskExecutor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("MemberId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("MemberId");

                    b.ToTable("TaskExecutors", (string)null);
                });

            modelBuilder.Entity("TagTask", b =>
                {
                    b.Property<Guid>("TagsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TasksId")
                        .HasColumnType("uuid");

                    b.HasKey("TagsId", "TasksId");

                    b.HasIndex("TasksId");

                    b.ToTable("TagTask");
                });

            modelBuilder.Entity("CommentMember", b =>
                {
                    b.HasOne("MyToDo.Domain.Entities.Comment", null)
                        .WithMany()
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyToDo.Domain.Entities.Member", null)
                        .WithMany()
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyToDo.Domain.Entities.Comment", b =>
                {
                    b.HasOne("MyToDo.Domain.Entities.Task", "Task")
                        .WithMany("Comments")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Task");
                });

            modelBuilder.Entity("MyToDo.Domain.Entities.Member", b =>
                {
                    b.HasOne("MyToDo.Domain.Entities.Role", "Role")
                        .WithMany("Members")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("MyToDo.Domain.Entities.RolePermission", b =>
                {
                    b.HasOne("MyToDo.Domain.Entities.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyToDo.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyToDo.Domain.Entities.Task", b =>
                {
                    b.HasOne("MyToDo.Domain.Entities.TaskCreator", "Creator")
                        .WithOne("Task")
                        .HasForeignKey("MyToDo.Domain.Entities.Task", "CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyToDo.Domain.Entities.TaskExecutor", "Executor")
                        .WithOne("Task")
                        .HasForeignKey("MyToDo.Domain.Entities.Task", "ExecutorId");

                    b.Navigation("Creator");

                    b.Navigation("Executor");
                });

            modelBuilder.Entity("MyToDo.Domain.Entities.TaskCreator", b =>
                {
                    b.HasOne("MyToDo.Domain.Entities.Member", null)
                        .WithMany()
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyToDo.Domain.Entities.TaskExecutor", b =>
                {
                    b.HasOne("MyToDo.Domain.Entities.Member", null)
                        .WithMany()
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TagTask", b =>
                {
                    b.HasOne("MyToDo.Domain.Entities.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyToDo.Domain.Entities.Task", null)
                        .WithMany()
                        .HasForeignKey("TasksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyToDo.Domain.Entities.Role", b =>
                {
                    b.Navigation("Members");
                });

            modelBuilder.Entity("MyToDo.Domain.Entities.Task", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("MyToDo.Domain.Entities.TaskCreator", b =>
                {
                    b.Navigation("Task")
                        .IsRequired();
                });

            modelBuilder.Entity("MyToDo.Domain.Entities.TaskExecutor", b =>
                {
                    b.Navigation("Task")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
