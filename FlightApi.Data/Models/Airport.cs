using System;
namespace FlightApi.Data.Models
{
	public class Airport
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string IATA { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}

