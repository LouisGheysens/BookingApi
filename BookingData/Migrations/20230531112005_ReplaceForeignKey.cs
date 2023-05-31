using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingData.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Taxi_Drivers_DriverId1",
                table: "Taxi");

            migrationBuilder.DropIndex(
                name: "IX_Taxi_DriverId1",
                table: "Taxi");

            migrationBuilder.DropColumn(
                name: "DriverId1",
                table: "Taxi");

            migrationBuilder.DropColumn(
                name: "TaxiId",
                table: "Drivers");

            migrationBuilder.CreateIndex(
                name: "IX_Taxi_DriverId",
                table: "Taxi",
                column: "DriverId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Taxi_Drivers_DriverId",
                table: "Taxi",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Taxi_Drivers_DriverId",
                table: "Taxi");

            migrationBuilder.DropIndex(
                name: "IX_Taxi_DriverId",
                table: "Taxi");

            migrationBuilder.AddColumn<int>(
                name: "DriverId1",
                table: "Taxi",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TaxiId",
                table: "Drivers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Taxi_DriverId1",
                table: "Taxi",
                column: "DriverId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Taxi_Drivers_DriverId1",
                table: "Taxi",
                column: "DriverId1",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
