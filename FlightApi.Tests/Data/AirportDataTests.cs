using Xunit;
using FlightApi.Data;
using System.Linq;

namespace Flight.Api.Tests.Data
{
    public class AirportDataTests
    {
        [Fact]
        public void AirportData_AirportsList_CountIsCorrect()
        {
            int expectedAirportCount = 4;

            int actualAirportCount = AirportData.Airports.Count;

            Assert.Equal(expectedAirportCount, actualAirportCount);
        }

        [Theory]
        [InlineData(1, "Los Angeles International Airport", "LAX", 33.9416, -118.4085)]
        [InlineData(2, "John F. Kennedy International Airport", "JFK", 40.6413, -73.7781)]
        [InlineData(3, "Heathrow Airport", "LHR", 51.4700, -0.4543)]
        [InlineData(4, "Charles de Gaulle Airport", "CDG", 49.0097, 2.5479)]
        public void AirportData_AirportsList_PropertiesAreCorrect(int id, string name, string iata, double latitude, double longitude)
        {
            var airport = AirportData.Airports.FirstOrDefault(a => a.Id == id);

            Assert.NotNull(airport);
            Assert.Equal(name, airport.Name);
            Assert.Equal(iata, airport.IATA);
            Assert.Equal(latitude, airport.Latitude);
            Assert.Equal(longitude, airport.Longitude);
        }
    }
}
