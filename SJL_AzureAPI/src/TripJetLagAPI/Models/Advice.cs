using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripJetLagAPI.Models
{
    public class Advice
    {
        [Key]
        public int AdviceId { get; set; }
        public string AdviceText { get; set; }
        [Column(TypeName ="smalldatetime")]
        public DateTime? NotificationTime { get; set; }
        public int CategoryId { get; set; }
        public int TripLegId { get; set; }

        [ForeignKey("CategoryId")]
        public AdviceCategory Category { get; set; }
        [ForeignKey("TripLegId")]
        public TripLeg TripLeg { get; set; }
    }
}
