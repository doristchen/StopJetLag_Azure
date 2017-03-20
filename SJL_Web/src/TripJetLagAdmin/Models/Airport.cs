using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TripJetLagAdmin.Models
{

    public class Airport
    {
       
        public string AirportCode { get; set; }
        public string AirportName { get; set; }

        public virtual ICollection<TripLeg> TripLegArrivalAirportCodeNavigation { get; set; }
        public virtual ICollection<TripLeg> TripLegDepartureAirportCodeNavigation { get; set; }
        
    }
}
