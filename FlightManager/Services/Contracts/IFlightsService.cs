using FlightManager.Data;
using FlightManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Services.Contracts
{
    public interface IFlightsService
    {
        public Flight CreateFlight(FlightCreateViewModel model);
        public Flight DeleteFlight(Flight flight);
        public Flight GetFlight(string destinationCity, DateTime departureTime, string captainName);
        public Flight GetFlightById(Guid id);
        public List<Flight> GetAllFlights();
        public Flight UpdateFlight(FlightEditViewModel model);

    }
}
