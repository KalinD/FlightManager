using FlightManager.Data;
using FlightManager.Models;
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

        public Flight CreateFlight(FlightCreateViewModel model)
        {
            Flight flight = new Flight() {
                DestinationCity = model.DestinationCity,
                DepartureCity = model.DepartureCity,
                DepartureTime = model.DepartureTime,
                ArrivalTime = model.ArrivalTime,
                PlaneType = model.PlaneType,
                PlaneID = model.PlaneID,
                CaptainName = model.CaptainName,
                PlaneCapacity = model.PlaneCapacity,
                BusinessClassCapacity = model.BusinessClassCapacity,
                BusinessTicketsLeft = model.BusinessClassCapacity,
                TicketsLeft = model.PlaneCapacity
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

        public Flight UpdateFlight(FlightEditViewModel model)
        {
            Flight dbFlight = dBContext.Flights.Where(f => f.FlightID == model.FlightId).First();

            dbFlight.ArrivalTime = model.ArrivalTime;
            dbFlight.BusinessClassCapacity = model.BusinessClassCapacity;
            dbFlight.CaptainName = model.CaptainName;
            dbFlight.DepartureCity = model.DepartureCity;
            dbFlight.DepartureTime = model.DepartureTime;
            dbFlight.DestinationCity = model.DestinationCity;
            dbFlight.PlaneCapacity = model.PlaneCapacity;
            dbFlight.PlaneID = model.PlaneID;
            dbFlight.PlaneType = model.PlaneType;

            dBContext.Flights.Update(dbFlight);
            dBContext.SaveChanges();

            return dbFlight;
        }
    }
}
