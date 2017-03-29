using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripJetLagAPI.Models
{
    public class AdviceCategory
    {
        [Key]
        public int CategoryId { get; set; }
        [StringLength(500)]
        public string CategoryDescr { get; set; }
        [StringLength(500)]
        public string ImageIcon { get; set; }

        public ICollection<Advice> Advices { get; set; }
    }
}
