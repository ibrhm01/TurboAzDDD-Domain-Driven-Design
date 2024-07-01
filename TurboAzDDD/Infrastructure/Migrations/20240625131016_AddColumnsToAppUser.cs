using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnsToAppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "69cdfd6a-1a41-4e20-ae4a-a1e97643bbb9", null, "Admin", "ADMIN" },
                    { "9d081b7e-e056-427c-9996-c9b9d2f03268", null, "SuperAdmin", "SUPERADMIN" },
                    { "f433e675-810d-457d-864d-635bc6ce975a", null, "Member", "MEMBER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "69cdfd6a-1a41-4e20-ae4a-a1e97643bbb9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9d081b7e-e056-427c-9996-c9b9d2f03268");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f433e675-810d-457d-864d-635bc6ce975a");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");

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
    }
}
