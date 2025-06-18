using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DeliveryApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    Vehicle = table.Column<string>(type: "TEXT", nullable: false),
                    RBF = table.Column<double>(type: "REAL", nullable: false),
                    FreezingATEF = table.Column<double>(type: "REAL", nullable: false),
                    ColdATEF = table.Column<double>(type: "REAL", nullable: false),
                    WSEF = table.Column<double>(type: "REAL", nullable: false),
                    SnowEF = table.Column<double>(type: "REAL", nullable: false),
                    RainEF = table.Column<double>(type: "REAL", nullable: false),
                    Modified = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeatherConditions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    WmoCode = table.Column<long>(type: "INTEGER", nullable: true),
                    AirTemperature = table.Column<double>(type: "REAL", nullable: false),
                    WindSpeed = table.Column<double>(type: "REAL", nullable: false),
                    WeatherPhenomenon = table.Column<string>(type: "TEXT", nullable: false),
                    TimeStamp = table.Column<long>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherConditions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Fees",
                columns: new[] { "Id", "City", "ColdAtef", "FreezingAtef", "Modified", "Rbf", "RainEf", "SnowEf", "Vehicle", "Wsef" },
                values: new object[,]
                {
                    { 1, "Tallinn", 0.5, 1.0, new DateTime(2025, 6, 16, 0, 0, 0, 0, DateTimeKind.Local), 3.5, 0.5, 1.0, "Scooter", 0.0 },
                    { 2, "Tallinn", 0.5, 1.0, new DateTime(2025, 6, 16, 0, 0, 0, 0, DateTimeKind.Local), 3.0, 0.5, 1.0, "Bike", 0.5 },
                    { 3, "Tallinn", 0.0, 0.0, new DateTime(2025, 6, 16, 0, 0, 0, 0, DateTimeKind.Local), 4.0, 0.0, 0.0, "Car", 0.0 },
                    { 4, "Tartu", 0.5, 1.0, new DateTime(2025, 6, 16, 0, 0, 0, 0, DateTimeKind.Local), 3.0, 0.5, 1.0, "Scooter", 0.0 },
                    { 5, "Tartu", 0.5, 1.0, new DateTime(2025, 6, 16, 0, 0, 0, 0, DateTimeKind.Local), 2.5, 0.5, 1.0, "Bike", 0.5 },
                    { 6, "Tartu", 0.0, 0.0, new DateTime(2025, 6, 16, 0, 0, 0, 0, DateTimeKind.Local), 3.5, 0.0, 0.0, "Car", 0.0 },
                    { 7, "Pärnu", 0.5, 1.0, new DateTime(2025, 6, 16, 0, 0, 0, 0, DateTimeKind.Local), 2.5, 0.5, 1.0, "Scooter", 0.0 },
                    { 8, "Pärnu", 0.5, 1.0, new DateTime(2025, 6, 16, 0, 0, 0, 0, DateTimeKind.Local), 2.0, 0.5, 1.0, "Bike", 0.5 },
                    { 9, "Pärnu", 0.0, 0.0, new DateTime(2025, 6, 16, 0, 0, 0, 0, DateTimeKind.Local), 3.0, 0.0, 0.0, "Car", 0.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fees");

            migrationBuilder.DropTable(
                name: "WeatherConditions");
        }
    }
}
