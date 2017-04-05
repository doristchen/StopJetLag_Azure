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
    public class TripAdviceController : Controller
    {
        private readonly ITripAdviceRepository _tripadviceRepository;
        // GET: api/values
        public TripAdviceController(ITripAdviceRepository tripadviceRepository)
        {
            _tripadviceRepository = tripadviceRepository;
        }

        [HttpGet("{id}"), Route("~/api/TripLegs/{Id:int}/Advices")]
        public Task<IEnumerable<AdviceMobile>> GetByTripLegId(int id)
        {
            return _tripadviceRepository.GetAllByTripLegId(id);
        }

        [HttpGet("{id}"), Route("~/api/Trips/{Id:int}/Advices")]
        public Task<IEnumerable<AdviceMobile>> GetByTripId(int id)
        {
            return _tripadviceRepository.GetAllByTripId(id);
        }

        [HttpGet]
        public async Task<IEnumerable<Advice>> GetAll()
        {
            return await _tripadviceRepository.GetAll();
        }

        [HttpGet ("{id}")]
        //[Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _tripadviceRepository.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Advice item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (item == null || item.AdviceId != id)
            {
                return BadRequest();
            }

            var findItem = await _tripadviceRepository.Find(id);
            if (findItem == null)
            {
                return NotFound();
            }

            findItem.AdviceText = item.AdviceText;
            findItem.CategoryId = item.CategoryId;
            findItem.NotificationTime = item.NotificationTime;
            findItem.TripLegId = item.TripLegId;
            
            await _tripadviceRepository.Update(findItem);
            return new NoContentResult();
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Advice item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _tripadviceRepository.Add(item);
            return CreatedAtRoute("GetById", new { id = item.AdviceId }, item);
        }


        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var findItem = await _tripadviceRepository.Find(id);
            if (findItem == null)
            {
                return NotFound();
            }
            
            await _tripadviceRepository.Delete(id);
            return new NoContentResult();
        }
    }
}
