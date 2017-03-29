using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Common;
using TripJetLagAPI.Data;
using TripJetLagAPI.Models;


namespace TripJetLagAPI.MobileDataModels
{
    public class TripNoteRepository : ITripNoteRepository
    {
        private readonly MobileDataDBContext _context;
        private readonly TripJetLagDBContext _tcontext;

        public TripNoteRepository(MobileDataDBContext context,
                                  TripJetLagDBContext tcontext)
        {
            _context = context;
            _tcontext = tcontext;
        }

        private List<NoteMobile> NoteMobileBuilder(DbDataReader dataReader)
        {
            List<NoteMobile> results = new List<NoteMobile>();

            while (dataReader.Read())
            {
                NoteMobile newItem = new NoteMobile();

                newItem.NoteId = int.Parse(dataReader["NoteId"].ToString());
                newItem.Note = dataReader["Note"] is DBNull ?
                    null : dataReader["Note"].ToString();
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

        public IEnumerable<NoteMobile> GetAllByTripId(int id)
        {
            DbDataReader dataReader = DataAccess.QueryStoredProcdure("sp_GetNoteByTrip", id, _context);

            return NoteMobileBuilder(dataReader);
        }

        public IEnumerable<NoteMobile> GetAllByTripLegId(int id)
        {
            DbDataReader dataReader = DataAccess.QueryStoredProcdure("sp_GetNoteByTripLeg", id, _context);

            return NoteMobileBuilder(dataReader);

        }

        public LegNote Find (int id)
        {
            return _tcontext.LegNotes.FirstOrDefault(t => t.NoteId == id);
        }

        public IEnumerable<LegNote> GetAll()
        {
            return _tcontext.LegNotes.ToList();
        }

        public void Update(LegNote item)
        {
            _tcontext.LegNotes.Update(item);
            _tcontext.SaveChanges();
        }
    }
}
