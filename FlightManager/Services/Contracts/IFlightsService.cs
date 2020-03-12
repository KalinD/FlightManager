using FlightManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Services.Contracts
{
    public interface IFlightsService
    {
        public Flight CreateFlight(string destinationCity, string departureCity, DateTime departureTime, DateTime arrivalTime, string planeType, string planeID, string captainName, int planeCapacity, int businessClassCapacity);
        public Flight DeleteFlight(Flight flight);
        public Flight GetFlight(string destinationCity, DateTime departureTime, string captainName);
        public Flight GetFlightById(Guid id);
        public List<Flight> GetAllFlights();
        public Flight UpdateFlight(Guid id, Flight flight);

    }
}
