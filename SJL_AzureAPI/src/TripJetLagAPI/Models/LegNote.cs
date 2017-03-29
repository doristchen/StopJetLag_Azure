using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TripJetLagAPI.Models
{
    public class LegNote
    {
        [Key]
        public int NoteId { get; set; }
        public string Note { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? NoteRetrieved { get; set; }
        public bool ReadyToDeliver { get; set; }
        public bool DeliverLegNote { get; set; }
        [Required]
        public int TripLegId { get; set; }

        [ForeignKey("TripLegId")]
        public TripLeg TripLeg { get; set; }
    }
}
