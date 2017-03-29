using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TripJetLagAPI.Models;

namespace TripJetLagAPI.Data
{
    public class TripJetLagDBContext : DbContext
    {
        public TripJetLagDBContext(DbContextOptions<TripJetLagDBContext> options) :
            base(options)
        {
        }

        public DbSet<Advice> Advices { get; set; }
        public DbSet<AdviceCategory> AdviceCategories { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<LegNote> LegNotes { get; set; }
        public DbSet<Traveler> Travelers { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<TripLeg> TripLegs { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.Relational().TableName = entity.DisplayName();
            }

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
                entity.HasKey(e => e.TripLegId)
                  .ForSqlServerIsClustered(false);

                entity.HasIndex(e => new {e.TripId, e.Segment})
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
                    .WithMany(p => p.TripLegArrivalAirportCodeNavigation)
                    .HasForeignKey(d => d.ArrivalAirportCode)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_TripLeg_AAirport");

                entity.HasOne(d => d.DepartureAirportCodeNavigation)
                    .WithMany(p => p.TripLegDepartureAirportCodeNavigation)
                    .HasForeignKey(d => d.DepartureAirportCode)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_TripLeg_DAirport");

             });
        }
     }
}

