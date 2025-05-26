using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class CommentOneToOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "17f781e8-bdac-4372-9837-8b0fb10bc1a8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9d442d8e-7686-4983-b910-317afe620549");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ccd2b534-6639-4a11-a4d8-db56c82b80b3", null, "Admin", "ADMIN" },
                    { "fb77287a-5ea8-4263-bb66-fbe4ef3e443e", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ccd2b534-6639-4a11-a4d8-db56c82b80b3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fb77287a-5ea8-4263-bb66-fbe4ef3e443e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "17f781e8-bdac-4372-9837-8b0fb10bc1a8", null, "Admin", "ADMIN" },
                    { "9d442d8e-7686-4983-b910-317afe620549", null, "User", "USER" }
                });
        }
    }
}
