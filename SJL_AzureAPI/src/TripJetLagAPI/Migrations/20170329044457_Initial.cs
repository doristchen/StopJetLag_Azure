using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TripJetLagAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdviceCategory",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryDescr = table.Column<string>(maxLength: 500, nullable: true),
                    ImageIcon = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdviceCategory", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Airport",
                columns: table => new
                {
                    AirportCode = table.Column<string>(type: "char(3)", nullable: false),
                    AirportName = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airport", x => x.AirportCode);
                });

            migrationBuilder.CreateTable(
                name: "Traveler",
                columns: table => new
                {
                    TravelerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 50, nullable: true),
                    LastName = table.Column<string>(maxLength: 50, nullable: true)
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
                    NotesRetrieved = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ReadyToDeliver = table.Column<bool>(nullable: false, defaultValueSql: "0"),
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
                    TripLegId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArrivalAirportCode = table.Column<string>(type: "char(3)", nullable: false),
                    ArrivalDate = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    DepartureAirportCode = table.Column<string>(type: "char(3)", nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    Segment = table.Column<int>(nullable: false),
                    TripId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripLeg", x => x.TripLegId)
                        .Annotation("SqlServer:Clustered", false);
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
                        name: "FK_TripLeg_Trip_TripId",
                        column: x => x.TripId,
                        principalTable: "Trip",
                        principalColumn: "TripId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Advice",
                columns: table => new
                {
                    AdviceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdviceText = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false),
                    NotificationTime = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    TripLegId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advice", x => x.AdviceId);
                    table.ForeignKey(
                        name: "FK_Advice_AdviceCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "AdviceCategory",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Advice_TripLeg_TripLegId",
                        column: x => x.TripLegId,
                        principalTable: "TripLeg",
                        principalColumn: "TripLegId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LegNote",
                columns: table => new
                {
                    NoteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DeliverLegNote = table.Column<bool>(nullable: false, defaultValueSql: "0"),
                    Note = table.Column<string>(nullable: true),
                    NoteRetrieved = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ReadyToDeliver = table.Column<bool>(nullable: false, defaultValueSql: "0"),
                    TripLegId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegNote", x => x.NoteId);
                    table.ForeignKey(
                        name: "FK_LegNote_TripLeg_TripLegId",
                        column: x => x.TripLegId,
                        principalTable: "TripLeg",
                        principalColumn: "TripLegId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Advice_CategoryId",
                table: "Advice",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Advice_TripLegId",
                table: "Advice",
                column: "TripLegId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_TripLegId",
                table: "LegNote",
                column: "TripLegId",
                unique: true);

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
                name: "IX_TripLeg_TripId",
                table: "TripLeg",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_TripLeg_Segment",
                table: "TripLeg",
                columns: new[] { "TripId", "Segment" },
                unique: true)
                .Annotation("SqlServer:Clustered", true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Advice");

            migrationBuilder.DropTable(
                name: "LegNote");

            migrationBuilder.DropTable(
                name: "AdviceCategory");

            migrationBuilder.DropTable(
                name: "TripLeg");

            migrationBuilder.DropTable(
                name: "Airport");

            migrationBuilder.DropTable(
                name: "Trip");

            migrationBuilder.DropTable(
                name: "Traveler");
        }
    }
}
