using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripJetLagAPI.Models
{
    public class Airport
    {
        [Key]
        public string AirportCode { get; set; }
        [StringLength(200)]
        public string AirportName { get; set; }

     }
}
