using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedDataToRolesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "35e5fb04-32bf-4af2-b268-1cb72b1674d2", null, "SuperAdmin", "SUPERADMIN" },
                    { "53c1a490-2302-45a8-aa3c-5f7c516281b1", null, "Admin", "ADMIN" },
                    { "afd2b920-3ca2-4b4c-8837-a40496c49b76", null, "Member", "MEMBER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "35e5fb04-32bf-4af2-b268-1cb72b1674d2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "53c1a490-2302-45a8-aa3c-5f7c516281b1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afd2b920-3ca2-4b4c-8837-a40496c49b76");
        }
    }
}
