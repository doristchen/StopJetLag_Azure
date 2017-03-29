using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        private List<AdviceMobile> AdviceMobileBuilder (DbDataReader dataReader)
        {
            List<AdviceMobile> results = new List<AdviceMobile>();

            while (dataReader.Read())
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

            return results;
        }

        public IEnumerable<AdviceMobile> GetAllByTripId(int id)
        {
            DbDataReader dataReader = DataAccess.QueryStoredProcdure("sp_GetAdviceByTrip", id, _context);

            return AdviceMobileBuilder(dataReader);
        }

        public IEnumerable<AdviceMobile> GetAllByTripLegId(int id)
        {
            DbDataReader dataReader = DataAccess.QueryStoredProcdure("sp_GetAdviceByTripLeg", id, _context);

            return AdviceMobileBuilder(dataReader);

        }

        public Advice Find(int id)
        {
            return _tcontext.Advices.FirstOrDefault(t => t.AdviceId == id);
        }

        public IEnumerable<Advice> GetAll()
        {
            return _tcontext.Advices.ToList();
        }

        public void Update(Advice item)
        {
            _tcontext.Advices.Update(item);
            _tcontext.SaveChanges();
        }

    }
}
