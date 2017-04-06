using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using TripJetLagAPI.Data;
using TripJetLagAPI.Models;
using Microsoft.EntityFrameworkCore;


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

        private async Task<List<NoteMobile>> NoteMobileBuilder(DbDataReader dataReader)
        {
            List<NoteMobile> results = new List<NoteMobile>();

            while (await dataReader.ReadAsync())
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
            dataReader.Dispose();
            return results;
        }

        public async Task<IEnumerable<NoteMobile>> GetAllByTripId(int id)
        {
            return await NoteMobileBuilder(await DataAccess.QueryStoredProcdure("sp_GetNoteByTrip", id, _context));
        }

        public async Task<IEnumerable<NoteMobile>> GetAllByTripLegId(int id)
        {
            return await NoteMobileBuilder(await DataAccess.QueryStoredProcdure("sp_GetNoteByTripLeg", id, _context));
        }

        public async Task<LegNote> Find (int id)
        {
            return await _tcontext.LegNotes.AsNoTracking()
                .FirstOrDefaultAsync(t => t.NoteId == id);
        }

        public async Task<IEnumerable<LegNote>> GetAll()
        {
            return await _tcontext.LegNotes.AsNoTracking()
                .ToListAsync();
        }

        public async Task<int> Update(LegNote item)
        {
            _tcontext.LegNotes.Update(item);
            return await _tcontext.SaveChangesAsync();
        }

        public async Task<int>Delete(int id)
        {
            var legNote = await Find(id);
            _tcontext.LegNotes.Remove(legNote);
            return await _tcontext.SaveChangesAsync();
        }

        public async Task<int>Add(LegNote item)
        {
            _tcontext.Add(item);
            return await _tcontext.SaveChangesAsync();
        }

    }
}
