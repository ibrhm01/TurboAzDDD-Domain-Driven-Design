using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNumberOfOwnerColumnToVehicle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_NumberOfOwners_NumberOfOwnerId",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_NumberOfSeats_NumberOfSeatId",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "NumberOfOwners");

            migrationBuilder.DropTable(
                name: "NumberOfSeats");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_NumberOfOwnerId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_NumberOfSeatId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "NumberOfOwnerId",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "NumberOfSeatId",
                table: "Vehicles",
                newName: "NumberOfOwners");

            migrationBuilder.AlterColumn<string>(
                name: "VinCode",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberOfOwners",
                table: "Vehicles",
                newName: "NumberOfSeatId");

            migrationBuilder.AlterColumn<string>(
                name: "VinCode",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfOwnerId",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "NumberOfOwners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    NumberOfOwnerName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumberOfOwners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NumberOfSeats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    NumberOfSeatName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumberOfSeats", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_NumberOfOwnerId",
                table: "Vehicles",
                column: "NumberOfOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_NumberOfSeatId",
                table: "Vehicles",
                column: "NumberOfSeatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_NumberOfOwners_NumberOfOwnerId",
                table: "Vehicles",
                column: "NumberOfOwnerId",
                principalTable: "NumberOfOwners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_NumberOfSeats_NumberOfSeatId",
                table: "Vehicles",
                column: "NumberOfSeatId",
                principalTable: "NumberOfSeats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
