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
                entity.Property(e => e.AirportCode).HasColumnType("char(3)");

            });

            modelBuilder.Entity<LegNote>(entity =>
            {
                entity.HasIndex(e => e.TripLegId)
                    .HasName("IX_Note_TripLegId")
                    .IsUnique();

                entity.Property(e => e.DeliverLegNote).HasDefaultValueSql("0");
                entity.Property(e => e.ReadyToDeliver).HasDefaultValueSql("0");

            });

            modelBuilder.Entity<Trip>(entity =>
            {
                entity.Property(e => e.ReadyToDeliver).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<TripLeg>(entity =>
            {
                entity.HasKey(e=>e.TripLegId)
                .ForSqlServerIsClustered(false);

                entity.HasIndex(e => new {e.TripId, e.Segment })
                     .IsUnique()
                     .ForSqlServerIsClustered(true)
                     .HasName("IX_TripLeg_Segment");


                entity.Property(e => e.ArrivalAirportCode)
                    .IsRequired()
                    .HasColumnType("char(3)");

                entity.Property(e => e.DepartureAirportCode)
                    .IsRequired()
                    .HasColumnType("char(3)");

                entity.HasOne(d => d.ArrivalAirportCodeNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.ArrivalAirportCode)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_TripLeg_AAirport");

                entity.HasOne(d => d.DepartureAirportCodeNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.DepartureAirportCode)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_TripLeg_DAirport");

            });
        }
    }
}
