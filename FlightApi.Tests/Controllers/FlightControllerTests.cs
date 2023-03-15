using System.Collections.Generic;
using System.Threading.Tasks;
using FlightApi.Controllers;
using FlightApi.Data.Models;
using FlightApi.Services.Interfaces;
using FlightApi.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace FlightApi.Tests
{
    public class FlightControllerTests
    {
        private readonly FlightController _controller;
        private readonly Mock<IFlightService> _flightServiceMock;
        private readonly FlightApi.Data.Models.Flight _flight;

        public FlightControllerTests()
        {
            _flightServiceMock = new Mock<IFlightService>();
            _controller = new FlightController(_flightServiceMock.Object);
            _flight = new FlightApi.Data.Models.Flight
            {

                Id = 1,
                Name = "Flight 1",
                OriginAirportId = 1,
                DestinationAirportId = 2,
                DepartureTime = DateTime.Now,
                ArrivalTime = DateTime.Now.AddHours(6),
                Airline = "Airline A",
                OriginAirport = new Airport
                {
                    Id = 1,
                    Name = "Airport A",
                    Latitude = 40.7128,
                    Longitude = -74.0060
                },
                DestinationAirport = new Airport
                {
                    Id = 2,
                    Name = "Airport B",
                    Latitude = 37.7749,
                    Longitude = -122.4194
                }

            };
        }

        [Fact]
        public async Task GetFlights_ReturnsCorrectData()
        {
            // Arrange
        var flights = new List<FlightApi.Data.Models.Flight>
                    {
        new FlightApi.Data.Models.Flight
        {
            Id = 1,
            Name = "Flight 1",
            OriginAirportId = 1,
            DestinationAirportId = 2,
            DepartureTime = DateTime.Now,
            ArrivalTime = DateTime.Now.AddHours(6),
            Airline = "Airline A",
            OriginAirport = new Airport
            {
                Id = 1,
                Name = "Airport A",
                Latitude = 40.7128,
                Longitude = -74.0060
            },
            DestinationAirport = new Airport
            {
                Id = 2,
                Name = "Airport B",
                Latitude = 37.7749,
                Longitude = -122.4194
            }
        },
        new FlightApi.Data.Models.Flight
        {
            Id = 2,
            Name = "Flight 2",
            OriginAirportId = 3,
            DestinationAirportId = 4,
            DepartureTime = DateTime.Now,
            ArrivalTime = DateTime.Now.AddHours(2),
            Airline = "Airline B",
            OriginAirport = new Airport
            {
                Id = 3,
                Name = "Airport C",
                Latitude = 51.5074,
                Longitude = -0.1278
            },
            DestinationAirport = new Airport
            {
                Id = 4,
                Name = "Airport D",
                Latitude = 48.8566,
                Longitude = 2.3522
            }
        }
    };


            _flightServiceMock.Setup(service => service.GetAllFlightsAsync()).ReturnsAsync(flights);
            // Act
            var result = await _controller.GetFlights();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var resultFlights = Assert.IsAssignableFrom<IEnumerable<dynamic>>(okResult.Value);

            Assert.Equal(flights.Count, resultFlights.Count());

            for (int i = 0; i < flights.Count; i++)
            {
                var flight = flights[i];
                var resultFlight = resultFlights.ElementAt(i);

                Assert.Equal(flight.Id, (int)resultFlight.GetType().GetProperty("Id").GetValue(resultFlight, null));
                Assert.Equal(flight.Name, (string)resultFlight.GetType().GetProperty("Name").GetValue(resultFlight, null));
                Assert.Equal(flight.OriginAirport.Name, (string)resultFlight.GetType().GetProperty("DepartureAirport").GetValue(resultFlight, null));
                Assert.Equal(flight.DestinationAirport.Name, (string)resultFlight.GetType().GetProperty("ArrivalAirport").GetValue(resultFlight, null));
                Assert.Equal(flight.GetDistanceBetweenAirports(), (double)resultFlight.GetType().GetProperty("Distance").GetValue(resultFlight, null), 1);
            }
        }

        [Fact]
        public async Task GetFlight_ReturnsCorrectData()
        {
            // Arrange
           

            int flightId = _flight.Id;

            _flightServiceMock.Setup(service => service.GetFlightByIdAsync(flightId)).ReturnsAsync(_flight);

            // Act
            var result = await _controller.GetFlight(flightId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var resultFlight = Assert.IsType<FlightApi.Data.Models.Flight>(okResult.Value);
            Assert.Equal(_flight, resultFlight);
        }

        [Fact]
        public async Task AddFlight_ReturnsCorrectData()
        {
            // Arrange

            _flightServiceMock.Setup(service => service.AddFlightAsync(_flight)).ReturnsAsync(_flight);
            // Act
            var result = await _controller.AddFlight(_flight);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var resultFlight = Assert.IsType<FlightApi.Data.Models.Flight>(createdAtActionResult.Value);
            Assert.Equal(_flight, resultFlight);
        }

        [Fact]
        public async Task UpdateFlight_ReturnsCorrectResult()
        {
            // Arrange
            
            int flightId = _flight.Id;


            _flightServiceMock.Setup(service => service.UpdateFlightAsync(_flight)).ReturnsAsync(true);

            // Act
            var result = await _controller.UpdateFlight(flightId, _flight);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteFlight_ReturnsCorrectResult()
        {
            // Arrange
            int flightId = 1;


            _flightServiceMock.Setup(service => service.DeleteFlightAsync(flightId)).ReturnsAsync(true);
            // Act
            var result = await _controller.DeleteFlight(flightId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

    }
}



