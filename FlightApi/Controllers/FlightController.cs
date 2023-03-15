using System.Collections.Generic;
using System.Threading.Tasks;
using FlightApi.Data.Models;
using FlightApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlightApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Flight>>> GetFlights()
        {
            var flights = await _flightService.GetAllFlightsAsync();

            var result = flights.Select(f => new
            {
                f.Id,
                f.Name,
                DepartureAirport = f.OriginAirport.Name,
                ArrivalAirport = f.DestinationAirport.Name,
                Distance = f.GetDistanceBetweenAirports()
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Flight>> GetFlight(int id)
        {
            var flight = await _flightService.GetFlightByIdAsync(id);
            if (flight == null)
            {
                return NotFound();
            }
            return Ok(flight);
        }

        [HttpPost]
        public async Task<ActionResult<Flight>> AddFlight(Flight flight)
        {
            var addedFlight = await _flightService.AddFlightAsync(flight);
            return CreatedAtAction(nameof(GetFlight), new { id = addedFlight.Id }, addedFlight);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFlight(int id, Flight flight)
        {
            if (id != flight.Id)
            {
                return BadRequest();
            }

            bool result = await _flightService.UpdateFlightAsync(flight);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            bool result = await _flightService.DeleteFlightAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
