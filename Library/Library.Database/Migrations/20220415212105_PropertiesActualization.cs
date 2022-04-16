using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Database.Migrations
{
    public partial class PropertiesActualization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "RentInterval",
                table: "Orders",
                newName: "BorrowInterval");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BorrowInterval",
                table: "Orders",
                newName: "RentInterval");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
