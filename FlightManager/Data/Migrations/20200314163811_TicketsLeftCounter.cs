using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightManager.Data.Migrations
{
    public partial class TicketsLeftCounter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BusinessTicketsLeft",
                table: "Flights",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TicketsLeft",
                table: "Flights",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EditFlightViewModel",
                columns: table => new
                {
                    FlightId = table.Column<Guid>(nullable: false),
                    DestinationCity = table.Column<string>(nullable: true),
                    DepartureCity = table.Column<string>(nullable: true),
                    DepartureTime = table.Column<DateTime>(nullable: false),
                    ArrivalTime = table.Column<DateTime>(nullable: false),
                    PlaneType = table.Column<string>(nullable: true),
                    PlaneID = table.Column<string>(nullable: true),
                    CaptainName = table.Column<string>(nullable: true),
                    PlaneCapacity = table.Column<int>(nullable: false),
                    BusinessClassCapacity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditFlightViewModel", x => x.FlightId);
                });

            migrationBuilder.CreateTable(
                name: "UserDetailsViewModel",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    SSN = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetailsViewModel", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EditFlightViewModel");

            migrationBuilder.DropTable(
                name: "UserDetailsViewModel");

            migrationBuilder.DropColumn(
                name: "BusinessTicketsLeft",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "TicketsLeft",
                table: "Flights");
        }
    }
}
