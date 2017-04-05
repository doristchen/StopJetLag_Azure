using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripJetLagAPI.Models
{
    public class AdviceCategory
    {
        [Key]
        public virtual int CategoryId { get; set; }
        [StringLength(500)]
        public virtual string CategoryDescr { get; set; }
        [StringLength(500)]
        public virtual string ImageIcon { get; set; }

    }
}
