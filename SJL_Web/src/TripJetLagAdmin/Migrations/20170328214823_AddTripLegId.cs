using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TripJetLagAdmin.Migrations
{
    public partial class AddTripLegId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TripLeg_LegNote_NoteId",
                table: "TripLeg");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TripLeg",
                table: "TripLeg");

            migrationBuilder.DropIndex(
                name: "IX_TripLeg_NoteId",
                table: "TripLeg");

            migrationBuilder.DropColumn(
                name: "NoteId",
                table: "TripLeg");

            migrationBuilder.AddColumn<int>(
                name: "TripLegId",
                table: "TripLeg",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "TripLegId",
                table: "LegNote",
                nullable: false);
                

            migrationBuilder.AddPrimaryKey(
                name: "PK_TripLeg",
                table: "TripLeg",
                column: "TripLegId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_TripLeg_Segment",
                table: "TripLeg",
                columns: new[] { "TripId", "Segment" },
                unique: true)
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NotesRetrieved",
                table: "Trip",
                type: "smalldatetime",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Traveler",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Traveler",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Note_TripLegId",
                table: "LegNote",
                column: "TripLegId",
                unique: true);

            migrationBuilder.AlterColumn<string>(
                name: "AirportName",
                table: "Airport",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LegNote_TripLeg_TripLegId",
                table: "LegNote",
                column: "TripLegId",
                principalTable: "TripLeg",
                principalColumn: "TripLegId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LegNote_TripLeg_TripLegId",
                table: "LegNote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TripLeg",
                table: "TripLeg");

            migrationBuilder.DropIndex(
                name: "IX_TripLeg_Segment",
                table: "TripLeg");

            migrationBuilder.DropIndex(
                name: "IX_Note_TripLegId",
                table: "LegNote");

            migrationBuilder.DropColumn(
                name: "TripLegId",
                table: "TripLeg");

            migrationBuilder.DropColumn(
                name: "TripLegId",
                table: "LegNote");

            migrationBuilder.AddColumn<int>(
                name: "NoteId",
                table: "TripLeg",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TripLeg",
                table: "TripLeg",
                columns: new[] { "TripId", "Segment" });

            migrationBuilder.CreateIndex(
                name: "IX_TripLeg_NoteId",
                table: "TripLeg",
                column: "NoteId",
                unique: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NotesRetrieved",
                table: "Trip",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Traveler",
                maxLength: 50,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Traveler",
                maxLength: 50,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "AirportName",
                table: "Airport",
                maxLength: 50,
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_TripLeg_LegNote_NoteId",
                table: "TripLeg",
                column: "NoteId",
                principalTable: "LegNote",
                principalColumn: "NoteId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
