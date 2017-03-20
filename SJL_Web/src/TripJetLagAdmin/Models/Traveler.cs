using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TripJetLagAdmin.Models
{
    public class Traveler
    {
       
        public int TravelerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
               
        [Display(Name = "Traveler Name")]
        public string TravelerName
        {
            get
            {
                return FirstName  + " " + LastName;
            }
        }
        public ICollection<Trip> Trips { get; set; }
    }
}
