using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightManager.Data;
using FlightManager.Models;
using FlightManager.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FlightManager.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService reservationService;
        private readonly IFlightsService flightService;

        public ReservationController(IReservationService reservationService, IFlightsService flightService)
        {
            this.reservationService = reservationService;
            this.flightService = flightService;
        }

        public IActionResult Index()
        {
            ReservationsIndexViewModel model = new ReservationsIndexViewModel()
            {
                Reservations = reservationService.GetAllReservations().Select(r => new ReservationIndexViewModel()
                {
                    Name = r.FirstName + " " + r.LastName,
                    Email = r.Email,
                    DepartureCity = flightService.GetFlightById(r.FlightID).DepartureCity,
                    DepartureTime = flightService.GetFlightById(r.FlightID).DepartureTime,
                    DestinationCity = flightService.GetFlightById(r.FlightID).DestinationCity,
                    PhoneNumber = r.PhoneNumber,
                    TicketType = r.TicketType,
                }).ToList()
            };
            return View(model);
        }

        public IActionResult Create()
        {
            ReservationCreateViewModel model = new ReservationCreateViewModel()
            {
                Flights = flightService.GetAllFlights().Select(f => new ReservationFlightCreateViewModel()
                {
                    FlightID = f.FlightID,
                    DepartureCity = f.DepartureCity,
                    DepartureTime = f.DepartureTime,
                    DestinationCity = f.DestinationCity,
                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(Guid flightId, int ticketCount, string email, string firstName, string secondName, string lastName, string SSN, string phoneNumber,
            string nationality, string ticketType)
        {
            Flight flight = flightService.GetFlightById(flightId);
            if (ticketType == "business")
            {
                flight.BusinessClassCapacity -= ticketCount;
            }
            else
            {
                flight.PlaneCapacity -= ticketCount;
            }
            reservationService.CreateReservation(email, firstName, secondName, lastName, SSN, phoneNumber, nationality, ticketType, flight);
            return RedirectToAction("Index", "Home");
        }
    }
}