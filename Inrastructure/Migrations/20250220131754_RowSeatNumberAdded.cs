using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RowSeatNumberAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RowNumber",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SeatNumber",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "MovieActors",
                keyColumn: "Id",
                keyValue: 2,
                column: "Role",
                value: "Captain America");

            migrationBuilder.UpdateData(
                table: "Sessions",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTimeEnd",
                value: new DateTime(2025, 2, 15, 21, 1, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Sessions",
                columns: new[] { "Id", "DateTimeEnd", "DateTimeStart", "HallId", "MovieId", "Price", "ReservedPlaces" },
                values: new object[,]
                {
                    { 2, new DateTime(2025, 2, 16, 17, 58, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 16, 15, 30, 0, 0, DateTimeKind.Unspecified), 2, 2, 12.99f, 5 },
                    { 3, new DateTime(2025, 2, 17, 23, 1, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 17, 20, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, 16.5f, 10 },
                    { 4, new DateTime(2025, 2, 18, 20, 13, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 18, 17, 45, 0, 0, DateTimeKind.Unspecified), 1, 2, 14.5f, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sessions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sessions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Sessions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "RowNumber",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "SeatNumber",
                table: "Tickets");

            migrationBuilder.UpdateData(
                table: "MovieActors",
                keyColumn: "Id",
                keyValue: 2,
                column: "Role",
                value: "Captain Amerika");

            migrationBuilder.UpdateData(
                table: "Sessions",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTimeEnd",
                value: new DateTime(2025, 2, 15, 21, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
