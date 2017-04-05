using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TripJetLagAdmin.Data;
using TripJetLagAdmin.Models;

namespace TripJetLagAdmin.Controllers
{
    public class TripsController : Controller
    {
        private readonly TripJetLagDBContext _context;

        public TripsController(TripJetLagDBContext context)
        {
            _context = context;
        }

        // GET: Trips
        public async Task<IActionResult> Index()
        {
            var tripJetLagDBContext = _context.Trips.Include(t => t.Traveler)
                                .Include(t => t.TripLegs).ThenInclude(t => t.DepartureAirportCodeNavigation)
                                .Include(t => t.TripLegs).ThenInclude(t => t.ArrivalAirportCodeNavigation)
                                .Include(t => t.TripLegs).ThenInclude(t => t.LegNote);


            return View(await tripJetLagDBContext.ToListAsync());
        }

        // GET: Trips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trips.SingleOrDefaultAsync(m => m.TripId == id);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        // GET: Trips/Create
        public IActionResult Create()
        {
            ViewData["TravelerId"] = new SelectList(_context.Travelers, "TravelerId", "FirstName");
            return View();
        }

        // POST: Trips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TripId, TravelerId, NotesRetrieved,ReadyToDeliver")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trip);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["TravelerId"] = new SelectList(_context.Travelers, "TravelerId", "FirstName", trip.TravelerId);
            return View(trip);
        }

        // GET: Trips/Edit/5
        public async Task<IActionResult> Edit(int? id, int? id2)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trips.SingleOrDefaultAsync(m => m.TripId == id);
            if (trip == null)
            {
                return NotFound();
            }
            ViewData["TravelerId"] = new SelectList(_context.Travelers, "TravelerId", "TravelerName", trip.TravelerId);
            return View(trip);
        }

        // POST: Trips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TripId, TravelerId, NotesRetrieved,ReadyToDeliver")] Trip trip)
        {
            if (id != trip.TripId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripExists(trip.TripId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["TravelerId"] = new SelectList(_context.Travelers, "TravelerId", "FirstName", trip.TravelerId);
            return View(trip);
        }

        // GET: Trips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trips.SingleOrDefaultAsync(m => m.TripId == id);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        // POST: Trips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trip = await _context.Trips.SingleOrDefaultAsync(m => m.TripId == id);
            _context.Trips.Remove(trip);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TripExists(int id)
        {
            return _context.Trips.Any(e => e.TripId == id);
        }
    }
}
