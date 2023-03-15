using System;
namespace FlightApi.Data.Models
{
	public class Flight
	{
        public int Id { get; set; }
        public string Name { get; set; } 
        public int OriginAirportId { get; set; }
        public int DestinationAirportId { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string Airline { get; set; }

        // Navigation properties for airports
        public Airport OriginAirport { get; set; }
        public Airport DestinationAirport { get; set; }

     
        public double GetDistanceBetweenAirports()
        {
            double R = 6371; // Radius of the earth in km
            double lat1 = OriginAirport.Latitude;
            double lat2 = OriginAirport.Longitude;
            double dLat = DestinationAirport.Latitude;
            double dLon = DestinationAirport.Longitude;

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                        Math.Cos(lat1) * Math.Cos(lat2) *
                        Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = R * c; // Distance in km

            return distance;
        }

        
    }
}

