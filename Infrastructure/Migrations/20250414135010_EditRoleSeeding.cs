
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditRoleSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0312a64f-f09d-4f57-8665-42aa66483f5b",
                column: "NormalizedName",
                value: "SELLER");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3b30be5a-525f-4ca2-877e-950378d8a5c1",
                column: "NormalizedName",
                value: "MANAGER");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "67d3150a-0ae9-4fe3-81f5-9bcaad261855",
                column: "NormalizedName",
                value: "BUYER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0312a64f-f09d-4f57-8665-42aa66483f5b",
                column: "NormalizedName",
                value: "0312A64F-F09D-4F57-8665-42AA66483F5B");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3b30be5a-525f-4ca2-877e-950378d8a5c1",
                column: "NormalizedName",
                value: "3B30BE5A-525F-4CA2-877E-950378D8A5C1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "67d3150a-0ae9-4fe3-81f5-9bcaad261855",
                column: "NormalizedName",
                value: "67D3150A-0AE9-4FE3-81F5-9BCAAD261855");
        }
    }
}
