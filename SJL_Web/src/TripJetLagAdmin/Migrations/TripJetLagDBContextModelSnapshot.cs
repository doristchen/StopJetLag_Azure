using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TripJetLagAdmin.Data;

namespace TripJetLagAdmin.Migrations
{
    [DbContext(typeof(TripJetLagDBContext))]
    partial class TripJetLagDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TripJetLagAdmin.Models.Airport", b =>
                {
                    b.Property<string>("AirportCode")
                        .HasColumnType("char(3)");

                    b.Property<string>("AirportName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("AirportCode")
                        .HasName("PK_Airport");

                    b.ToTable("Airport");
                });

            modelBuilder.Entity("TripJetLagAdmin.Models.LegNote", b =>
                {
                    b.Property<int>("NoteId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Note");

                    b.Property<DateTime?>("NoteRetrieved")
                        .HasColumnType("smalldatetime");

                    b.Property<bool>("ReadyToDeliver")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("0");

                    b.HasKey("NoteId")
                        .HasName("PK_LegNote");

                    b.ToTable("LegNote");
                });

            modelBuilder.Entity("TripJetLagAdmin.Models.Traveler", b =>
                {
                    b.Property<int>("TravelerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("TravelerId");

                    b.ToTable("Traveler");
                });

            modelBuilder.Entity("TripJetLagAdmin.Models.Trip", b =>
                {
                    b.Property<int>("TripId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("TravelerId");

                    b.HasKey("TripId");

                    b.HasIndex("TravelerId")
                        .HasName("IX_Trip_TravelerId");

                    b.ToTable("Trip");
                });

            modelBuilder.Entity("TripJetLagAdmin.Models.TripLeg", b =>
                {
                    b.Property<int>("TripId");

                    b.Property<int>("Segment");

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

                    b.Property<int?>("NoteId");

                    b.HasKey("TripId", "Segment")
                        .HasName("PK_TripLeg");

                    b.HasIndex("ArrivalAirportCode");

                    b.HasIndex("DepartureAirportCode");

                    b.HasIndex("NoteId")
                        .IsUnique()
                        .HasName("IX_TripLeg_NoteId");

                    b.HasIndex("TripId");

                    b.ToTable("TripLeg");
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

                    b.HasOne("TripJetLagAdmin.Models.LegNote", "Note")
                        .WithOne("TripLeg")
                        .HasForeignKey("TripJetLagAdmin.Models.TripLeg", "NoteId");

                    b.HasOne("TripJetLagAdmin.Models.Trip", "Trip")
                        .WithMany("TripLegs")
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
