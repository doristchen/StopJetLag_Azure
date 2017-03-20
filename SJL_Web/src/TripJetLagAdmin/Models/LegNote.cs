using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripJetLagAdmin.Models
{
    public class LegNote
    {
       
        public int NoteId { get; set; }
        public string Note { get; set; }
        [Display(Name = "Note Retrieved")]
        public DateTime? NoteRetrieved { get; set; }
        [Display(Name = "Ready To Deliver")]
        public bool ReadyToDeliver { get; set; }

        [Display(Name = "Deliver Leg Note")]
        public bool DeliverLegNote { get; set; }

        public virtual TripLeg TripLeg { get; set; }
    }
}
