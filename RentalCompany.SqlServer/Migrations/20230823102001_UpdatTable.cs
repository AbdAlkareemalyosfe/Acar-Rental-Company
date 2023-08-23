using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalCompany.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class UpdatTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriversSet_CarsSet_CarId",
                table: "DriversSet");

            migrationBuilder.DropIndex(
                name: "IX_DriversSet_CarId",
                table: "DriversSet");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "DriversSet");

            migrationBuilder.CreateIndex(
                name: "IX_CarsSet_DriverId",
                table: "CarsSet",
                column: "DriverId",
                unique: true,
                filter: "[DriverId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CarsSet_DriversSet_DriverId",
                table: "CarsSet",
                column: "DriverId",
                principalTable: "DriversSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarsSet_DriversSet_DriverId",
                table: "CarsSet");

            migrationBuilder.DropIndex(
                name: "IX_CarsSet_DriverId",
                table: "CarsSet");

            migrationBuilder.AddColumn<Guid>(
                name: "CarId",
                table: "DriversSet",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DriversSet_CarId",
                table: "DriversSet",
                column: "CarId",
                unique: true,
                filter: "[CarId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_DriversSet_CarsSet_CarId",
                table: "DriversSet",
                column: "CarId",
                principalTable: "CarsSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
