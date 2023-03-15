using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FlightApi.Data;
using FlightApi.Data.Models;
using FlightApi.Services.Interfaces;

namespace FlightApi.Services.Services
{
    public class FlightService : IFlightService
    {
        private readonly ApplicationDbContext _context;

        public FlightService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Flight>> GetAllFlightsAsync()
        {
            return await _context.Flights
        .Include(f => f.OriginAirport)
        .Include(f => f.DestinationAirport)
        .ToListAsync();
        }

        

        public async Task<Flight> GetFlightByIdAsync(int id)
        {
            return await _context.Flights.FindAsync(id);

        }

        public async Task<Flight> AddFlightAsync(Flight flight)
        {
            _context.Flights.Add(flight);
            await _context.SaveChangesAsync();
            return flight;
        }

        public async Task<bool> UpdateFlightAsync(Flight flight)
        {
            _context.Entry(flight).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlightExists(flight.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        private bool FlightExists(int id)
        {
            return _context.Flights.Any(e => e.Id == id);
        }

        public async Task<bool> DeleteFlightAsync(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight == null)
            {
                return false;
            }

            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();

            return true;
        }
        
    }
}
