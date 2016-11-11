using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheWorld.Model;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    [Route("api/trips")]
    [Authorize]
    public class TripsController : Controller
    {
        private ILogger<TripsController> _logger;
        private IWorldRepository _repository;

        public TripsController(IWorldRepository repository, ILogger<TripsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                var trips = _repository.GetUserTripsWithStops(User.Identity.Name);
                var results = Mapper.Map<IEnumerable<TripViewModel>>(trips);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Faield to get all trips { ex }");
                return BadRequest("Error Occurred");
            }

        }
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]TripViewModel theTrip)
        {
            if (ModelState.IsValid)
            {
                // save to db
                var newTrip = Mapper.Map<Trip>(theTrip);
                newTrip.UserName = User.Identity.Name;
                _repository.AddTrip(newTrip);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/trips/{theTrip.Name}", Mapper.Map<TripViewModel>(newTrip));
                }
                else
                {
                    return BadRequest("Failed to save changes");
                }
            }

            return BadRequest(ModelState);
        }
    }
}
