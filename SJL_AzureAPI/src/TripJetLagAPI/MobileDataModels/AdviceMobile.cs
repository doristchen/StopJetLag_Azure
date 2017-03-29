using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripJetLagAPI.MobileDataModels
{
    public class AdviceMobile
    {
        public int AdviceId { get; set; }
        public int CategoryId {get; set;}
        public string AdviceText {get; set;}
        public DateTime? NotificationTime { get; set; }
        public string ImageIcon { get; set; }
        public int TripId { get; set; } 
        public int TripLegId { get; set; }
        public int Segment { get; set; }
        public DateTime? DepartureDate { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
    }

}
