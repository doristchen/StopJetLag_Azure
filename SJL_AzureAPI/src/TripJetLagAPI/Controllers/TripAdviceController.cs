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
        public IEnumerable<AdviceMobile> GetByTripLegId(int id)
        {
            return _tripadviceRepository.GetAllByTripLegId(id);
        }

        [HttpGet("{id}"), Route("~/api/Trips/{Id:int}/Advices")]
        public IEnumerable<AdviceMobile> GetByTripId(int id)
        {
            return _tripadviceRepository.GetAllByTripId(id);
        }

        [HttpGet]
        public IEnumerable<Advice> GetAll()
        {
            return _tripadviceRepository.GetAll();
        }

        //[HttpGet ("{id}")]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            var item = _tripadviceRepository.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Advice item)
        {
            if (item == null || item.AdviceId != id)
            {
                return BadRequest();
            }

            var findItem = _tripadviceRepository.Find(id);
            if (findItem == null)
            {
                return NotFound();
            }

            findItem.AdviceText = item.AdviceText;
            findItem.CategoryId = item.CategoryId;
            findItem.NotificationTime = item.NotificationTime;
            findItem.TripLegId = item.TripLegId;
            
            _tripadviceRepository.Update(findItem);
            return new NoContentResult();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Advice value)
        {
        }
        

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
