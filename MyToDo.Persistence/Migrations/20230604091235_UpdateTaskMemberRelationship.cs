using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyToDo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTaskMemberRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TaskCreators_CreatorId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TaskExecutors_ExecutorId",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "TaskCreators");

            migrationBuilder.DropTable(
                name: "TaskExecutors");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_CreatorId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_ExecutorId",
                table: "Tasks");
            
            migrationBuilder.AlterColumn<Guid>(
                name: "ExecutorId",
                table: "Tasks",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_CreatorId",
                table: "Tasks",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ExecutorId",
                table: "Tasks",
                column: "ExecutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Members_CreatorId",
                table: "Tasks",
                column: "CreatorId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Members_ExecutorId",
                table: "Tasks",
                column: "ExecutorId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Members_CreatorId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Members_ExecutorId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_CreatorId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_ExecutorId",
                table: "Tasks");
            
            migrationBuilder.AlterColumn<Guid>(
                name: "ExecutorId",
                table: "Tasks",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateTable(
                name: "TaskCreators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    MemberId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskCreators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskCreators_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskExecutors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    MemberId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskExecutors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskExecutors_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            
            migrationBuilder.CreateIndex(
                name: "IX_Tasks_CreatorId",
                table: "Tasks",
                column: "CreatorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ExecutorId",
                table: "Tasks",
                column: "ExecutorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskCreators_Id",
                table: "TaskCreators",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskCreators_MemberId",
                table: "TaskCreators",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskExecutors_Id",
                table: "TaskExecutors",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskExecutors_MemberId",
                table: "TaskExecutors",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_TaskCreators_CreatorId",
                table: "Tasks",
                column: "CreatorId",
                principalTable: "TaskCreators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_TaskExecutors_ExecutorId",
                table: "Tasks",
                column: "ExecutorId",
                principalTable: "TaskExecutors",
                principalColumn: "Id");
        }
    }
}
