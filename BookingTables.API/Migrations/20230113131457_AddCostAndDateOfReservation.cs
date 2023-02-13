using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingTablesAPI.Migrations
{
    public partial class AddCostAndDateOfReservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOfReservation",
                table: "Orders",
                newName: "StartOfReservation");

            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "Tables",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CostOfOrder",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndOfReservation",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "CostOfOrder",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "EndOfReservation",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "StartOfReservation",
                table: "Orders",
                newName: "DateOfReservation");
        }
    }
}
