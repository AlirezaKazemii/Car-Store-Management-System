using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0312a64f-f09d-4f57-8665-42aa66483f5b", "0312a64f-f09d-4f57-8665-42aa66483f5b", "Seller", "0312A64F-F09D-4F57-8665-42AA66483F5B" },
                    { "3b30be5a-525f-4ca2-877e-950378d8a5c1", "3b30be5a-525f-4ca2-877e-950378d8a5c1", "Manager", "3B30BE5A-525F-4CA2-877E-950378D8A5C1" },
                    { "67d3150a-0ae9-4fe3-81f5-9bcaad261855", "67d3150a-0ae9-4fe3-81f5-9bcaad261855", "Buyer", "67D3150A-0AE9-4FE3-81F5-9BCAAD261855" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0312a64f-f09d-4f57-8665-42aa66483f5b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3b30be5a-525f-4ca2-877e-950378d8a5c1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "67d3150a-0ae9-4fe3-81f5-9bcaad261855");
        }
    }
}
