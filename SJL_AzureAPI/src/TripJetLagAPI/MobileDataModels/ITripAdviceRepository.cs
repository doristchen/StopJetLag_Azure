using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripJetLagAPI.Models;

namespace TripJetLagAPI.MobileDataModels
{
    public interface ITripAdviceRepository
    {
        Task <IEnumerable<AdviceMobile>> GetAllByTripId(int id);
        Task <IEnumerable<AdviceMobile>> GetAllByTripLegId(int id);
        Task <IEnumerable<Advice>> GetAll();
        Task <Advice>Find(int id);
        Task <int> Update(Advice item);
        Task <int> Delete(int id);
        Task<int> Add(Advice item);


    }
}

