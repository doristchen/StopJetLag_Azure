using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc; 
using System.Threading.Tasks;
using TripJetLagAPI.Models;

namespace TripJetLagAPI.MobileDataModels
{
    public interface ITripNoteRepository
    {
        Task<IEnumerable<NoteMobile>> GetAllByTripId(int id);
        Task<IEnumerable<NoteMobile>> GetAllByTripLegId(int id);
        Task<IEnumerable<LegNote>> GetAll();
        Task<LegNote> Find(int id);
        Task<int> Update(LegNote item);
        Task<int> Delete(int id);
        Task<int> Add(LegNote item);

    }
}
