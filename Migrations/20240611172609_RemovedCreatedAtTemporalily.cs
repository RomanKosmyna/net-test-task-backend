using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace net_test_task_backend.Migrations
{
    /// <inheritdoc />
    public partial class RemovedCreatedAtTemporalily : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c923814-8581-448c-8cc0-f1671f31a313");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "929902ce-2c32-4e63-9514-1b2930ef7dd1");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Urls");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Urls",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "773d7fc4-8479-4987-ba33-a4d675959ab3", null, "User", "USER" },
                    { "b4d4ac79-ff6f-4d78-97ed-429b7d6190b2", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "773d7fc4-8479-4987-ba33-a4d675959ab3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4d4ac79-ff6f-4d78-97ed-429b7d6190b2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedBy",
                table: "Urls",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Urls",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2c923814-8581-448c-8cc0-f1671f31a313", null, "User", "USER" },
                    { "929902ce-2c32-4e63-9514-1b2930ef7dd1", null, "Admin", "ADMIN" }
                });
        }
    }
}
