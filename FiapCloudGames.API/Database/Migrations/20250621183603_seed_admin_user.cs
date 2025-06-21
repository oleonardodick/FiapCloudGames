using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FiapCloudGames.API.Database.Migrations
{
    /// <inheritdoc />
    public partial class seed_admin_user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("1bd7d258-b3fd-4e95-985f-811173518b30"),
                column: "CreatedAt",
                value: new DateTime(2025, 6, 21, 18, 36, 2, 264, DateTimeKind.Utc).AddTicks(9212));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("93f46947-3ff4-4ed2-a3eb-4fcec16014eb"),
                column: "CreatedAt",
                value: new DateTime(2025, 6, 21, 18, 36, 2, 264, DateTimeKind.Utc).AddTicks(9225));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Name", "Password", "RoleId" },
                values: new object[] { new Guid("440f02c7-1a65-4d78-a688-2d761373bad1"), new DateTime(2025, 6, 21, 18, 36, 2, 265, DateTimeKind.Utc).AddTicks(2572), "admin@fcg.com", "Admin", "$2a$11$W8znD/qgMC7JK4OVRBgRcezlrEaSjIe3ycxLH/byk.rAflMmndqgG", new Guid("1bd7d258-b3fd-4e95-985f-811173518b30") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("440f02c7-1a65-4d78-a688-2d761373bad1"));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("1bd7d258-b3fd-4e95-985f-811173518b30"),
                column: "CreatedAt",
                value: new DateTime(2025, 4, 21, 17, 47, 12, 677, DateTimeKind.Utc).AddTicks(7805));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("93f46947-3ff4-4ed2-a3eb-4fcec16014eb"),
                column: "CreatedAt",
                value: new DateTime(2025, 4, 21, 17, 47, 12, 677, DateTimeKind.Utc).AddTicks(7815));
        }
    }
}
