using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripJetLagAPI.Models;

namespace TripJetLagAPI.MobileDataModels
{
    public interface ITripNoteRepository
    {
        IEnumerable<NoteMobile> GetAllByTripId(int id);
        IEnumerable<NoteMobile> GetAllByTripLegId(int id);
        IEnumerable<LegNote> GetAll();
        LegNote Find(int id);
        void Update(LegNote item);

    }
}
