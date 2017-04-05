using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripJetLagAdmin.Models
{
    public class LegNote
    {

        [Key]
        public int NoteId { get; set; }
        public string Note { get; set; }
        [Display(Name = "Note Retrieved")]
        [Column(TypeName = "smalldatetime")]
        public DateTime? NoteRetrieved { get; set; }
        [Display(Name = "Ready To Deliver")]
        public bool ReadyToDeliver { get; set; }
        [Display(Name = "Deliver Leg Note")]
        public bool DeliverLegNote { get; set; }
        [Required]
        public int TripLegId { get; set; }

        [ForeignKey("TripLegId")]
        public TripLeg TripLeg { get; set; }
    }
}
