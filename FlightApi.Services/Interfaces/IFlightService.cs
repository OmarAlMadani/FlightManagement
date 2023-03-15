using System.Collections.Generic;
using FlightApi.Data.Models;

namespace FlightApi.Services.Interfaces
{
    public interface IFlightService
    {
        Task<IEnumerable<Flight>> GetAllFlightsAsync();
        Task<Flight> GetFlightByIdAsync(int id);
        Task<Flight> AddFlightAsync(Flight flight);
        Task<bool> UpdateFlightAsync(Flight flight);
        Task<bool> DeleteFlightAsync(int id);
    }
}
