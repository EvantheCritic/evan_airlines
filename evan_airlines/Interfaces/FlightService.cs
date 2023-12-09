using evan_airlines.Models;
using Microsoft.AspNetCore.Mvc;

namespace evan_airlines.Interfaces
{
    public class FlightService
    {
        private readonly AppDbContext context;
        private readonly List<FlightModel> allFlights; // Initialize this with your actual data

        public FlightService(AppDbContext _context)
        {
            // Initialize _allFlights with your actual data or fetch it from a database
            context = _context;
            allFlights = context.Flights.ToList();
        }

        public List<FlightModel> GetAllFlights()
        {
            return allFlights;
        }

        public List<FlightModel> SearchFlights(string _departure, string _arrival)
        {
            if (_departure == null && _arrival == null)
            {
                return allFlights;
            }
            else if (_arrival == null)
            {
                return allFlights
                .Where(flight => flight.departure.Contains(_departure))
                .ToList();
            }
            else if (_departure == null)
            {
                return allFlights
                .Where(flight => flight.arrival.Contains(_arrival))
                .ToList();
            }
            else
            {
                return allFlights
                .Where(flight => flight.departure.Contains(_departure) && flight.arrival.Contains(_arrival))
                .ToList();
            }
        }
    }
}
