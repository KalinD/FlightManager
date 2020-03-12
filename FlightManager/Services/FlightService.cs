using FlightManager.Data;
using FlightManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Services
{
    public class FlightService : IFlightsService
    {
        private readonly FlightManagerDbContext dBContext;

        public FlightService(FlightManagerDbContext context)
        {
            dBContext = context;
        }

        public Flight CreateFlight(string destinationCity, string departureCity, DateTime departureTime, DateTime arrivalTime, string planeType, string planeID, string captainName, int planeCapacity, int businessClassCapacity)
        {
            Flight flight = new Flight() {
                DestinationCity = destinationCity,
                DepartureCity = departureCity,
                DepartureTime = departureTime,
                ArrivalTime = arrivalTime,
                PlaneType = planeType,
                PlaneID = planeID,
                CaptainName = captainName,
                PlaneCapacity = planeCapacity,
                BusinessClassCapacity = businessClassCapacity
            };

            dBContext.Flights.Add(flight);
            dBContext.SaveChanges();

            return flight;
        }

        public Flight DeleteFlight(Flight flight)
        {
            dBContext.Flights.Remove(flight);
            dBContext.SaveChanges();

            return flight;
        }

        public List<Flight> GetAllFlights()
        {
            return dBContext.Flights.ToList();
        }

        public Flight GetFlight(string destinationCity, DateTime departureTime, string departureCity)
        {
            return dBContext.Flights.Where(f => f.DestinationCity == destinationCity && f.DepartureTime == departureTime && f.DepartureCity == departureCity).First();
        }

        public Flight GetFlightById(Guid id) { 
            return dBContext.Flights.Where(f => f.FlightID == id).First();
        }

        public Flight UpdateFlight(Guid id, Flight flight)
        {
            Flight dbFlight = dBContext.Flights.Where(f => f.FlightID == id).First();

            dbFlight.ArrivalTime = flight.ArrivalTime;
            dbFlight.BusinessClassCapacity = flight.BusinessClassCapacity;
            dbFlight.CaptainName = flight.CaptainName;
            dbFlight.DepartureCity = flight.DepartureCity;
            dbFlight.DepartureTime = flight.DepartureTime;
            dbFlight.DestinationCity = flight.DestinationCity;
            dbFlight.PlaneCapacity = flight.PlaneCapacity;
            dbFlight.PlaneID = flight.PlaneID;
            dbFlight.PlaneType = flight.PlaneType;

            dBContext.Flights.Update(dbFlight);
            dBContext.SaveChanges();

            return flight;
        }
    }
}
