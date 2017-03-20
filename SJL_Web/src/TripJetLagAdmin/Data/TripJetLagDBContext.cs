using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TripJetLagAdmin.Models;

namespace TripJetLagAdmin.Data
{
    public class TripJetLagDBContext : DbContext
    {
        public TripJetLagDBContext(DbContextOptions<TripJetLagDBContext> options) :
            base(options)
        {
        }


        public DbSet<Traveler> Travelers { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<TripLeg> TripLegs { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<LegNote> LegNotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Traveler>().ToTable("Traveler");
            modelBuilder.Entity<Trip>().ToTable("Trip");
            modelBuilder.Entity<TripLeg>().ToTable("TripLeg");
            modelBuilder.Entity<Airport>().ToTable("Airport");
            modelBuilder.Entity<LegNote>().ToTable("LegNote");

            modelBuilder.Entity<Airport>(entity =>
            {
                entity.HasKey(e => e.AirportCode)
                    .HasName("PK_Airport");

                entity.Property(e => e.AirportCode).HasColumnType("char(3)");

                entity.Property(e => e.AirportName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<LegNote>(entity =>
            {
                entity.HasKey(e => e.NoteId)
                    .HasName("PK_LegNote");

                entity.Property(e => e.NoteRetrieved).HasColumnType("smalldatetime");

                entity.Property(e => e.ReadyToDeliver).HasDefaultValueSql("0");

                entity.Property(e => e.DeliverLegNote).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<Traveler>(entity =>
            {
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Trip>(entity =>
            {
                entity.HasIndex(e => e.TravelerId)
                    .HasName("IX_Trip_TravelerId");

                entity.HasOne(d => d.Traveler)
                    .WithMany(p => p.Trips)
                    .HasForeignKey(d => d.TravelerId);

                entity.Property(e => e.ReadyToDeliver).HasDefaultValueSql("0");

            });

            modelBuilder.Entity<TripLeg>(entity =>
            {
                entity.HasKey(e => new { e.TripId, e.Segment })
                    .HasName("PK_TripLeg");

                entity.HasIndex(e => e.NoteId)
                    .HasName("IX_TripLeg_NoteId")
                    .IsUnique();

                entity.Property(e => e.ArrivalAirportCode)
                    .IsRequired()
                    .HasColumnType("char(3)");

                entity.Property(e => e.ArrivalDate).HasColumnType("smalldatetime");

                entity.Property(e => e.DepartureAirportCode)
                    .IsRequired()
                    .HasColumnType("char(3)");

                entity.Property(e => e.DepartureDate).HasColumnType("smalldatetime");

                entity.HasOne(d => d.ArrivalAirportCodeNavigation)
                    .WithMany(p => p.TripLegArrivalAirportCodeNavigation)
                    .HasForeignKey(d => d.ArrivalAirportCode)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_TripLeg_AAirport");

                entity.HasOne(d => d.DepartureAirportCodeNavigation)
                    .WithMany(p => p.TripLegDepartureAirportCodeNavigation)
                    .HasForeignKey(d => d.DepartureAirportCode)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_TripLeg_DAirport");

                entity.HasOne(d => d.Note)
                    .WithOne(p => p.TripLeg)
                    .HasForeignKey<TripLeg>(d => d.NoteId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.TripLegs)
                    .HasForeignKey(d => d.TripId);
            });
        }
    }
}
