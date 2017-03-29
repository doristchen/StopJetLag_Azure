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
        public IEnumerable<NoteMobile> GetByTripLegId(int id)
        {
            return _tripnoteRepository.GetAllByTripLegId(id);
        }
               
        [HttpGet("{id}"), Route("~/api/Trips/{Id:int}/Notes")]
        public IEnumerable<NoteMobile> GetByTripId(int id)
        {
            return _tripnoteRepository.GetAllByTripId(id);
        }

        [HttpGet]
        public IEnumerable<LegNote>GetAll()
        {
            return _tripnoteRepository.GetAll();
        }

        //[HttpGet ("{id}", Name="GetTripNote")]
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            var item = _tripnoteRepository.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }


        //[HttpPut("{id}")]
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update(int id, [FromBody] LegNote item)
        {
            if (item == null || item.NoteId != id)
            {
                return BadRequest();
            }

            var findItem = _tripnoteRepository.Find(id);
            if (findItem == null)
            {
                return NotFound();
            }

            findItem.Note = item.Note;
            findItem.NoteRetrieved = item.NoteRetrieved;
            findItem.ReadyToDeliver = item.ReadyToDeliver;
            findItem.TripLegId = item.TripLegId;
            findItem.NoteId = item.NoteId;
            
            _tripnoteRepository.Update(findItem);
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]LegNote value)
        {
        }

    }
}
