using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripJetLagAdmin.Models
{
    public class TripLeg
    {
        [Display(Name = "Id")]
        public int TripId { get; set; }
        public int Segment { get; set; }
        public string ArrivalAirportCode { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public string DepartureAirportCode { get; set; }
        public DateTime? DepartureDate { get; set; }
        public int? NoteId {get; set;}


        public Airport ArrivalAirportCodeNavigation { get; set; }
        public Airport DepartureAirportCodeNavigation { get; set; }
        public LegNote Note { get; set; }
        public Trip Trip { get; set; }
 
   
        [Display(Name = "Trip Leg")]
        public string TripLegText
        {
            get
            {
                return DepartureAirportCodeNavigation.AirportName + " to "  
                     + ArrivalAirportCodeNavigation.AirportName;
            }
        }

    }
}
