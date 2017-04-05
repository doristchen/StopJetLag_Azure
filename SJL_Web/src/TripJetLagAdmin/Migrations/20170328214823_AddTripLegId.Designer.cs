using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TripJetLagAdmin.Data;

namespace TripJetLagAdmin.Migrations
{
    [DbContext(typeof(TripJetLagDBContext))]
    [Migration("20170328214823_AddTripLegId")]
    partial class AddTripLegId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TripJetLagAdmin.Models.Airport", b =>
                {
                    b.Property<string>("AirportCode")
                        .HasColumnType("char(3)");

                    b.Property<string>("AirportName")
                        .HasAnnotation("MaxLength", 200);

                    b.HasKey("AirportCode");

                    b.ToTable("Airport");
                });

            modelBuilder.Entity("TripJetLagAdmin.Models.LegNote", b =>
                {
                    b.Property<int>("NoteId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("DeliverLegNote")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("0");

                    b.Property<string>("Note");

                    b.Property<DateTime?>("NoteRetrieved")
                        .HasColumnType("smalldatetime");

                    b.Property<bool>("ReadyToDeliver")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("0");

                    b.Property<int>("TripLegId");

                    b.HasKey("NoteId");

                    b.HasIndex("TripLegId")
                        .IsUnique()
                        .HasName("IX_Note_TripLegId");

                    b.ToTable("LegNote");
                });

            modelBuilder.Entity("TripJetLagAdmin.Models.Traveler", b =>
                {
                    b.Property<int>("TravelerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("LastName")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("TravelerId");

                    b.ToTable("Traveler");
                });

            modelBuilder.Entity("TripJetLagAdmin.Models.Trip", b =>
                {
                    b.Property<int>("TripId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("NotesRetrieved")
                        .HasColumnType("smalldatetime");

                    b.Property<bool>("ReadyToDeliver")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("0");

                    b.Property<int>("TravelerId");

                    b.HasKey("TripId");

                    b.HasIndex("TravelerId");

                    b.ToTable("Trip");
                });

            modelBuilder.Entity("TripJetLagAdmin.Models.TripLeg", b =>
                {
                    b.Property<int>("TripLegId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ArrivalAirportCode")
                        .IsRequired()
                        .HasColumnType("char(3)");

                    b.Property<DateTime?>("ArrivalDate")
                        .HasColumnType("smalldatetime");

                    b.Property<string>("DepartureAirportCode")
                        .IsRequired()
                        .HasColumnType("char(3)");

                    b.Property<DateTime?>("DepartureDate")
                        .HasColumnType("smalldatetime");

                    b.Property<int>("Segment");

                    b.Property<int>("TripId");

                    b.HasKey("TripLegId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("ArrivalAirportCode");

                    b.HasIndex("DepartureAirportCode");

                    b.HasIndex("TripId");

                    b.HasIndex("TripId", "Segment")
                        .IsUnique()
                        .HasName("IX_TripLeg_Segment")
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.ToTable("TripLeg");
                });

            modelBuilder.Entity("TripJetLagAdmin.Models.LegNote", b =>
                {
                    b.HasOne("TripJetLagAdmin.Models.TripLeg", "TripLeg")
                        .WithOne("LegNote")
                        .HasForeignKey("TripJetLagAdmin.Models.LegNote", "TripLegId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TripJetLagAdmin.Models.Trip", b =>
                {
                    b.HasOne("TripJetLagAdmin.Models.Traveler", "Traveler")
                        .WithMany("Trips")
                        .HasForeignKey("TravelerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TripJetLagAdmin.Models.TripLeg", b =>
                {
                    b.HasOne("TripJetLagAdmin.Models.Airport", "ArrivalAirportCodeNavigation")
                        .WithMany("TripLegArrivalAirportCodeNavigation")
                        .HasForeignKey("ArrivalAirportCode")
                        .HasConstraintName("FK_TripLeg_AAirport");

                    b.HasOne("TripJetLagAdmin.Models.Airport", "DepartureAirportCodeNavigation")
                        .WithMany("TripLegDepartureAirportCodeNavigation")
                        .HasForeignKey("DepartureAirportCode")
                        .HasConstraintName("FK_TripLeg_DAirport");

                    b.HasOne("TripJetLagAdmin.Models.Trip", "Trip")
                        .WithMany("TripLegs")
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
