using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FiapCloudGames.API.Database.Migrations
{
    /// <inheritdoc />
    public partial class SeedingTheRolesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("1bd7d258-b3fd-4e95-985f-811173518b30"), new DateTime(2025, 4, 20, 1, 14, 1, 170, DateTimeKind.Utc).AddTicks(5960), "Admin" },
                    { new Guid("93f46947-3ff4-4ed2-a3eb-4fcec16014eb"), new DateTime(2025, 4, 20, 1, 14, 1, 170, DateTimeKind.Utc).AddTicks(5963), "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("1bd7d258-b3fd-4e95-985f-811173518b30"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("93f46947-3ff4-4ed2-a3eb-4fcec16014eb"));
        }
    }
}
