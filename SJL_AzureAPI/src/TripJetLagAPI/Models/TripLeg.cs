﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripJetLagAPI.Models
{
    public class TripLeg
    {
        [Key]
        public int TripLegId { get; set; }
        public int Segment { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? DepartureDate { get; set; }
        public string DepartureAirportCode { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? ArrivalDate { get; set; }
        public string ArrivalAirportCode { get; set; }
        public int TripId { get; set; }

        [ForeignKey("TripId")]
        public Trip Trip { get; set; }
        [ForeignKey("DepartureAirportCode")]
        public Airport ArrivalAirportCodeNavigation { get; set; }
        [ForeignKey("ArrivalAirportCode")]
        public Airport DepartureAirportCodeNavigation { get; set; }
        
    }
}
