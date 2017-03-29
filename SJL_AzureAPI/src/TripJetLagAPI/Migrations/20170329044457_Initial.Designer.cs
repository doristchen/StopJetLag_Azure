using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TripJetLagAPI.Data;

namespace TripJetLagAPI.Migrations
{
    [DbContext(typeof(TripJetLagDBContext))]
    [Migration("20170329044457_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TripJetLagAPI.Models.Advice", b =>
                {
                    b.Property<int>("AdviceId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AdviceText");

                    b.Property<int>("CategoryId");

                    b.Property<DateTime?>("NotificationTime")
                        .HasColumnType("smalldatetime");

                    b.Property<int>("TripLegId");

                    b.HasKey("AdviceId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("TripLegId");

                    b.ToTable("Advice");
                });

            modelBuilder.Entity("TripJetLagAPI.Models.AdviceCategory", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryDescr")
                        .HasAnnotation("MaxLength", 500);

                    b.Property<string>("ImageIcon")
                        .HasAnnotation("MaxLength", 500);

                    b.HasKey("CategoryId");

                    b.ToTable("AdviceCategory");
                });

            modelBuilder.Entity("TripJetLagAPI.Models.Airport", b =>
                {
                    b.Property<string>("AirportCode")
                        .HasColumnType("char(3)");

                    b.Property<string>("AirportName")
                        .HasAnnotation("MaxLength", 200);

                    b.HasKey("AirportCode");

                    b.ToTable("Airport");
                });

            modelBuilder.Entity("TripJetLagAPI.Models.LegNote", b =>
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

            modelBuilder.Entity("TripJetLagAPI.Models.Traveler", b =>
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

            modelBuilder.Entity("TripJetLagAPI.Models.Trip", b =>
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

            modelBuilder.Entity("TripJetLagAPI.Models.TripLeg", b =>
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

            modelBuilder.Entity("TripJetLagAPI.Models.Advice", b =>
                {
                    b.HasOne("TripJetLagAPI.Models.AdviceCategory", "Category")
                        .WithMany("Advices")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TripJetLagAPI.Models.TripLeg", "TripLeg")
                        .WithMany("Advices")
                        .HasForeignKey("TripLegId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TripJetLagAPI.Models.LegNote", b =>
                {
                    b.HasOne("TripJetLagAPI.Models.TripLeg", "TripLeg")
                        .WithOne("LegNote")
                        .HasForeignKey("TripJetLagAPI.Models.LegNote", "TripLegId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TripJetLagAPI.Models.Trip", b =>
                {
                    b.HasOne("TripJetLagAPI.Models.Traveler", "Traveler")
                        .WithMany("Trips")
                        .HasForeignKey("TravelerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TripJetLagAPI.Models.TripLeg", b =>
                {
                    b.HasOne("TripJetLagAPI.Models.Airport", "ArrivalAirportCodeNavigation")
                        .WithMany("TripLegArrivalAirportCodeNavigation")
                        .HasForeignKey("ArrivalAirportCode")
                        .HasConstraintName("FK_TripLeg_AAirport");

                    b.HasOne("TripJetLagAPI.Models.Airport", "DepartureAirportCodeNavigation")
                        .WithMany("TripLegDepartureAirportCodeNavigation")
                        .HasForeignKey("DepartureAirportCode")
                        .HasConstraintName("FK_TripLeg_DAirport");

                    b.HasOne("TripJetLagAPI.Models.Trip", "Trip")
                        .WithMany("TripLegs")
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
