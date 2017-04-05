using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripJetLagAdmin.Models
{
    public class Trip
    {
        [Display(Name = "Id")]

        [Key]
        public int TripId { get; set; }
        [Display(Name = "Notes Retrieved")]
        [Column(TypeName = "smalldatetime")]
        public DateTime? NotesRetrieved { get; set; }
        [Display(Name = "Ready To Deliver")]
        public bool ReadyToDeliver { get; set; }
        public int TravelerId { get; set; }
        
        [ForeignKey("TravelerId")]
        public Traveler Traveler {get; set;}
        public ICollection<TripLeg> TripLegs { get; set; }
    }
}
