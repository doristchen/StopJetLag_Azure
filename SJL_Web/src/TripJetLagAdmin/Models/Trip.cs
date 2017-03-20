using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripJetLagAdmin.Models
{
    public class Trip
    {
        [Display(Name = "Id")]
        public int TripId { get; set; }
        public int TravelerId { get; set; }
        [Display(Name = "Notes Retrieved")]
        public DateTime? NotesRetrieved { get; set; }
        [Display(Name = "Ready To Deliver")]
        public bool ReadyToDeliver { get; set; }
        public ICollection<TripLeg> TripLegs { get; set; }
        public Traveler Traveler {get; set;}

    }
}
