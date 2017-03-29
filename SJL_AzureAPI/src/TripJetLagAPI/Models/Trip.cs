using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TripJetLagAPI.Models
{
    public partial class Trip
    {
        [Key]
        public int TripId { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? NotesRetrieved { get; set; }
        public bool ReadyToDeliver { get; set; }
        public int TravelerId { get; set; }

        [ForeignKey("TravelerId")]
        public Traveler Traveler { get; set; }
        public ICollection<TripLeg> TripLegs { get; set; }
    }
}
