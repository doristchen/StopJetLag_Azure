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
    public class LegNotesController : Controller
    {
        private readonly TripJetLagDBContext _context;

        public LegNotesController(TripJetLagDBContext context)
        {
            _context = context;
        }

        // GET: LegNotes
        public async Task<IActionResult> Index(int? id)
        {

            if (id != null)
            {
                var tripIndexJetLagDBContext = _context.LegNotes.Include(t => t.TripLeg)
                                 .ThenInclude(t => t.DepartureAirportCodeNavigation)
                                .Include(t => t.TripLeg)
                                 .ThenInclude(t => t.ArrivalAirportCodeNavigation)
                                .Include(t => t.TripLeg)
                                 .ThenInclude(t => t.Trip)
                                .ThenInclude(t => t.Traveler)
                                .Where(t => t.TripLeg.TripId == id.Value);

                return View(await tripIndexJetLagDBContext.ToListAsync());

            }
            else
            {
                var tripJetLagDBContext = _context.LegNotes.Include(t => t.TripLeg)
                                 .ThenInclude(t => t.DepartureAirportCodeNavigation)
                                .Include(t => t.TripLeg)
                                 .ThenInclude(t => t.ArrivalAirportCodeNavigation)
                                .Include(t => t.TripLeg)
                                 .ThenInclude(t => t.Trip)
                                .ThenInclude(t => t.Traveler);

                return View(await tripJetLagDBContext.ToListAsync());

            }

        }

        // GET: LegNotes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var legNote = await _context.LegNotes.SingleOrDefaultAsync(m => m.NoteId == id);
            if (legNote == null)
            {
                return NotFound();
            }

            return View(legNote);
        }

        // GET: LegNotes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LegNotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NoteId,Note,NoteRetrieved,ReadyToDeliver, DeliverLegNote")] LegNote legNote)
        {
            if (ModelState.IsValid)
            {
                _context.Add(legNote);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(legNote);
        }

        // GET: LegNotes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var legNote = await _context.LegNotes.Include(m => m.TripLeg)
                          .ThenInclude(m => m.DepartureAirportCodeNavigation)
                          .Include(m => m.TripLeg)
                          .ThenInclude(m => m.ArrivalAirportCodeNavigation)
                          .SingleOrDefaultAsync(m => m.NoteId == id);
            if (legNote == null)
            {
                return NotFound();
            }
            return View(legNote);
        }

        // POST: LegNotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NoteId,Note,NoteRetrieved,ReadyToDeliver, DeliverLegNote")] LegNote legNote)
        {
            if (id != legNote.NoteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(legNote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LegNoteExists(legNote.NoteId))
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
            return View(legNote);
        }

        // GET: LegNotes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var legNote = await _context.LegNotes.SingleOrDefaultAsync(m => m.NoteId == id);
            if (legNote == null)
            {
                return NotFound();
            }

            return View(legNote);
        }

        // POST: LegNotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var legNote = await _context.LegNotes.SingleOrDefaultAsync(m => m.NoteId == id);
            _context.LegNotes.Remove(legNote);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool LegNoteExists(int id)
        {
            return _context.LegNotes.Any(e => e.NoteId == id);
        }
    }
}
