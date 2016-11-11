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
using TheWorld.Servces;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    [Route("api/trips/{tripName}/stops")]
    [Authorize]
    public class StopsController : Controller
    {
        private GeoCoordsService _coordsService;
        private ILogger<StopsController> _logger;
        private IWorldRepository _repository;

        public StopsController(IWorldRepository repository,
            ILogger<StopsController> logger,
            GeoCoordsService coordsService)
        {
            _repository = repository;
            _logger = logger;
            _coordsService = coordsService;
        }

        [HttpGet("")]
        public IActionResult Get(string tripName)
        {
            try
            {
                var trip = _repository.GetTripByName(tripName, User.Identity.Name);
                return Ok(Mapper.Map<StopViewModel>(trip.Stops.OrderBy(s => s.Order).ToList()));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Faield to get all stops { ex }");
                return BadRequest("Error Occurred to get all stops");
            }

        }

        [HttpPost("")]
        public async Task<IActionResult> Post(string tripName, [FromBody]StopViewModel svm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newStop = Mapper.Map<Stop>(svm);
                    var result = await _coordsService.GetCoordsAsync(newStop.Name);
                    if (!result.Success)
                    {
                        _logger.LogError($"Failed to get corrdinates- {result.Message}");
                    }
                    else
                    {
                        newStop.Latitude = result.Latitude;
                        newStop.Longitude = result.Longitude;

                        _repository.AddStop(tripName, newStop, User.Identity.Name);
                        if (await _repository.SaveChangesAsync())
                        {
                            return Created($"api/trips/{tripName}/stops/{newStop.Name}", Mapper.Map<StopViewModel>(newStop));

                        }
                    }
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Faield to save new stops { ex }");
            }

            return BadRequest("Error Occurred in saving stops");
        }
    }
}
