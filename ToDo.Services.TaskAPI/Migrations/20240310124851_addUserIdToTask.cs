using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToDo.Services.TaskAPI.Migrations
{
    /// <inheritdoc />
    public partial class addUserIdToTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Tasks",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tasks");

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "Description", "DueDate", "IsCompleted", "Priority", "Title" },
                values: new object[,]
                {
                    { 1, "Draft and finalize the project proposal for client review.", new DateTime(2024, 3, 16, 12, 40, 23, 872, DateTimeKind.Utc).AddTicks(1950), false, 2, "Complete Project Proposal" },
                    { 2, "Participate in the weekly team meeting to discuss project updates.", new DateTime(2024, 3, 11, 12, 40, 23, 872, DateTimeKind.Utc).AddTicks(1994), false, 1, "Attend Team Meeting" },
                    { 3, "Create a presentation for the upcoming client meeting.", new DateTime(2024, 3, 14, 12, 40, 23, 872, DateTimeKind.Utc).AddTicks(1997), false, 2, "Prepare Presentation" }
                });
        }
    }
}
