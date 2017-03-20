using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TripJetLagAdmin.Migrations
{
    public partial class InitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airport",
                columns: table => new
                {
                    AirportCode = table.Column<string>(type: "char(3)", nullable: false),
                    AirportName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airport", x => x.AirportCode);
                });

            migrationBuilder.CreateTable(
                name: "LegNote",
                columns: table => new
                {
                    NoteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Note = table.Column<string>(nullable: true),
                    NoteRetrieved = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ReadyToDeliver = table.Column<bool>(nullable: false, defaultValueSql: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegNote", x => x.NoteId);
                });

            migrationBuilder.CreateTable(
                name: "Traveler",
                columns: table => new
                {
                    TravelerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Traveler", x => x.TravelerId);
                });

            migrationBuilder.CreateTable(
                name: "Trip",
                columns: table => new
                {
                    TripId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TravelerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trip", x => x.TripId);
                    table.ForeignKey(
                        name: "FK_Trip_Traveler_TravelerId",
                        column: x => x.TravelerId,
                        principalTable: "Traveler",
                        principalColumn: "TravelerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TripLeg",
                columns: table => new
                {
                    TripId = table.Column<int>(nullable: false),
                    Segment = table.Column<int>(nullable: false),
                    ArrivalAirportCode = table.Column<string>(type: "char(3)", nullable: false),
                    ArrivalDate = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    DepartureAirportCode = table.Column<string>(type: "char(3)", nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    NoteId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripLeg", x => new { x.TripId, x.Segment });
                    table.ForeignKey(
                        name: "FK_TripLeg_AAirport",
                        column: x => x.ArrivalAirportCode,
                        principalTable: "Airport",
                        principalColumn: "AirportCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TripLeg_DAirport",
                        column: x => x.DepartureAirportCode,
                        principalTable: "Airport",
                        principalColumn: "AirportCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TripLeg_LegNote_NoteId",
                        column: x => x.NoteId,
                        principalTable: "LegNote",
                        principalColumn: "NoteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TripLeg_Trip_TripId",
                        column: x => x.TripId,
                        principalTable: "Trip",
                        principalColumn: "TripId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trip_TravelerId",
                table: "Trip",
                column: "TravelerId");

            migrationBuilder.CreateIndex(
                name: "IX_TripLeg_ArrivalAirportCode",
                table: "TripLeg",
                column: "ArrivalAirportCode");

            migrationBuilder.CreateIndex(
                name: "IX_TripLeg_DepartureAirportCode",
                table: "TripLeg",
                column: "DepartureAirportCode");

            migrationBuilder.CreateIndex(
                name: "IX_TripLeg_NoteId",
                table: "TripLeg",
                column: "NoteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TripLeg_TripId",
                table: "TripLeg",
                column: "TripId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TripLeg");

            migrationBuilder.DropTable(
                name: "Airport");

            migrationBuilder.DropTable(
                name: "LegNote");

            migrationBuilder.DropTable(
                name: "Trip");

            migrationBuilder.DropTable(
                name: "Traveler");
        }
    }
}
