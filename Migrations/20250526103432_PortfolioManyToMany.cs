using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class PortfolioManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "673904f7-eaa4-4b22-a225-2fa5caaddaea");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c6ba47ad-9adf-46e3-ab81-f07ccbb83c45");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "17f781e8-bdac-4372-9837-8b0fb10bc1a8", null, "Admin", "ADMIN" },
                    { "9d442d8e-7686-4983-b910-317afe620549", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "673904f7-eaa4-4b22-a225-2fa5caaddaea", null, "Admin", "ADMIN" },
                    { "c6ba47ad-9adf-46e3-ab81-f07ccbb83c45", null, "User", "USER" }
                });
        }
    }
}
