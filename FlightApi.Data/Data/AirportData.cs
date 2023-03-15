using System.Collections.Generic;
using FlightApi.Data.Models;

namespace FlightApi.Data
{
    public static class AirportData
    {
        public static List<Airport> Airports = new List<Airport>
        {
                new Airport { Id = 1, Name = "Los Angeles International Airport", IATA = "LAX", Latitude = 33.9416, Longitude = -118.4085 },
                new Airport { Id = 2, Name = "John F. Kennedy International Airport", IATA = "JFK", Latitude = 40.6413, Longitude = -73.7781 },
                new Airport { Id = 3, Name = "Heathrow Airport", IATA = "LHR", Latitude = 51.4700, Longitude = -0.4543 },
                new Airport { Id = 4, Name = "Charles de Gaulle Airport", IATA = "CDG", Latitude = 49.0097, Longitude = 2.5479 },
               
            
        };
    }
}
