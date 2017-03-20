using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripJetLagAdmin.Models.ManageViewModels
{
    public class TripViewModel
    {
        public IEnumerable<Trip> Trips { get; set; }
        public IEnumerable<TripLeg> TripLegs { get; set; }
    }
}
