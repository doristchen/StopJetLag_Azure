using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripJetLagAPI.Models;

namespace TripJetLagAPI.MobileDataModels
{
    public interface ITripAdviceRepository
    {
        IEnumerable<AdviceMobile> GetAllByTripId(int id);
        IEnumerable<AdviceMobile> GetAllByTripLegId(int id);
        IEnumerable<Advice> GetAll();
        Advice Find(int id);
        void Update(Advice item);
    }
}

