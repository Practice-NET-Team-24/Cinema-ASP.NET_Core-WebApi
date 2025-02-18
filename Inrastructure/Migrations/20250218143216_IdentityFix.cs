using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IdentityFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "f39d012f-7e83-42cb-b64e-42c9a88c727a", "admin@example.com", false, false, null, "Admin", null, null, null, null, false, 0, null, false, null },
                    { 2, 0, "c4f8e583-b50c-43fe-ad97-1e2427a09588", "user1@example.com", false, false, null, "User1", null, null, null, null, false, 1, null, false, null }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "PlaceId", "PlaceId1", "SessionId", "UserId", "UserId1" },
                values: new object[] { 1, new DateTime(2025, 2, 10, 12, 0, 0, 0, DateTimeKind.Unspecified), 1, null, 1, 2, null });
        }
    }
}
