using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using TripJetLagAPI.Data;
using TripJetLagAPI.Models;

namespace TripJetLagAPI.MobileDataModels
{
    public class TripAdviceRepository : ITripAdviceRepository
    {
        private readonly MobileDataDBContext _context;
        private readonly TripJetLagDBContext _tcontext;

        public TripAdviceRepository(MobileDataDBContext context,
                                  TripJetLagDBContext tcontext)
        {
            _context = context;
            _tcontext = tcontext;
        }

        private async Task<List<AdviceMobile>> AdviceMobileBuilder (DbDataReader dataReader)
        {
            List<AdviceMobile> results = new List<AdviceMobile>();

           
            while (await dataReader.ReadAsync())
            {
                AdviceMobile newItem = new AdviceMobile();

                newItem.AdviceId = int.Parse(dataReader["AdviceId"].ToString());
                newItem.CategoryId = int.Parse(dataReader["CategoryId"].ToString());
                newItem.AdviceText = dataReader["AdviceText"] is DBNull ?
                    null:dataReader["AdviceText"].ToString();
                newItem.NotificationTime = DateTime.Parse(dataReader["NotificationTime"].ToString());
                newItem.ImageIcon = dataReader["ImageIcon"] is DBNull ?
                    null : dataReader["ImageIcon"].ToString();
                newItem.TripId = int.Parse(dataReader["TripId"].ToString());
                newItem.TripLegId = int.Parse(dataReader["TripLegId"].ToString());
                newItem.Segment = int.Parse(dataReader["Segment"].ToString());
                newItem.DepartureDate = DateTime.Parse(dataReader["DepartureDate"].ToString());
                newItem.ArrivalDate = DateTime.Parse(dataReader["ArrivalDate"].ToString());
                newItem.DepartureAirport = dataReader["DAirportName"] is DBNull ?
                    null : dataReader["DAirportName"].ToString();
                newItem.ArrivalAirport = dataReader["AAirportName"] is DBNull ?
                    null : dataReader["AAirportName"].ToString();

                results.Add(newItem);
            }

            dataReader.Dispose();
            return results;
        }

        public async Task<IEnumerable<AdviceMobile>> GetAllByTripId(int id)
        {
            return await AdviceMobileBuilder(await DataAccess.QueryStoredProcdure("sp_GetAdviceByTrip", id, _context));
        }

        public async Task<IEnumerable<AdviceMobile>> GetAllByTripLegId(int id)
        {
            return await AdviceMobileBuilder(await DataAccess.QueryStoredProcdure("sp_GetAdviceByTripLeg", id, _context));
        }

        public async Task<Advice> Find(int id)
        {
            return await _tcontext.Advices.AsNoTracking()
                .FirstOrDefaultAsync(t => t.AdviceId == id);
        }

        public async Task<IEnumerable<Advice>> GetAll()
        {
            return await _tcontext.Advices.Include(t => t.Category)
                .Include(t => t.TripLeg).ThenInclude(t => t.DepartureAirportCodeNavigation)
                .Include(t => t.TripLeg).ThenInclude(t => t.ArrivalAirportCodeNavigation)
                .Include(t => t.TripLeg).ThenInclude(t => t.Trip)
                .ThenInclude(t => t.Traveler).AsNoTracking().ToListAsync();
        }

        public async Task<int> Update(Advice item)
        {
            _tcontext.Advices.Update(item);
            return await _tcontext.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            var advice = await Find(id);
            _tcontext.Advices.Remove(advice);
            return await _tcontext.SaveChangesAsync();
        }

        public async Task<int> Add(Advice item)
        {
            _tcontext.Add(item);
            return await _tcontext.SaveChangesAsync();
        }

    }
}
