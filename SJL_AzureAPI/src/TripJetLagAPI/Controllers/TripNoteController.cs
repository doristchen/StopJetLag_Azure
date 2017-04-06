using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TripJetLagAPI.Models;
using TripJetLagAPI.MobileDataModels;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TripJetLagAPI.Controllers
{
    [Route("api/[controller]")]
    public class TripNoteController : Controller
    {
        private readonly ITripNoteRepository _tripnoteRepository;
        // GET: api/values
        public TripNoteController(ITripNoteRepository tripnoteRepository)
        {
            _tripnoteRepository = tripnoteRepository;
        }

        [HttpGet("{id}"), Route("~/api/TripLegs/{Id:int}/Notes")]
        public Task<IEnumerable<NoteMobile>> GetByTripLegId(int id)
        {
            return _tripnoteRepository.GetAllByTripLegId(id);
        }
               
        [HttpGet("{id}"), Route("~/api/Trips/{Id:int}/Notes")]
        public async Task<IEnumerable<NoteMobile>> GetByTripId(int id)
        {
            return await _tripnoteRepository.GetAllByTripId(id);
        }

        [HttpGet]
        public async Task<IEnumerable<LegNote>>GetAll()
        {
            return await _tripnoteRepository.GetAll();
        }

        [HttpGet, Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _tripnoteRepository.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }


        [HttpPut, Route("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] LegNote item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (item == null || item.NoteId != id)
            {
                return BadRequest();
            }

            var findItem = await _tripnoteRepository.Find(id);

            if (findItem == null)
            {
                return NotFound();
            }

            findItem.Note = item.Note;
            findItem.NoteRetrieved = item.NoteRetrieved;
            findItem.ReadyToDeliver = item.ReadyToDeliver;
            findItem.TripLegId = item.TripLegId;
            
            await _tripnoteRepository.Update(findItem);
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var findItem = await _tripnoteRepository.Find(id);
            if (findItem == null)
            {
                return NotFound();
            }

            await _tripnoteRepository.Delete(id);
            return new NoContentResult();
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]LegNote item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _tripnoteRepository.Add(item);
            return CreatedAtRoute("GetById", new { id = item.NoteId }, item);
        }
    }
}
