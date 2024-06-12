using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace net_test_task_backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateImageUrlAboutNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14803b59-f0ac-4f75-934f-e166bf8e7ffa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "794a60e8-9606-4ea5-8252-9cd8db87dca6");

            migrationBuilder.AlterColumn<string>(
                name: "ImageURL",
                table: "Abouts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1c5a2599-2ff7-44b5-a4e2-f9392f35ac4b", null, "Admin", "ADMIN" },
                    { "d64f62bc-b096-4d09-8447-5cd5c22ad3e6", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1c5a2599-2ff7-44b5-a4e2-f9392f35ac4b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d64f62bc-b096-4d09-8447-5cd5c22ad3e6");

            migrationBuilder.AlterColumn<string>(
                name: "ImageURL",
                table: "Abouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "14803b59-f0ac-4f75-934f-e166bf8e7ffa", null, "Admin", "ADMIN" },
                    { "794a60e8-9606-4ea5-8252-9cd8db87dca6", null, "User", "USER" }
                });
        }
    }
}
