using System;
using System.Linq;
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

        public IActionResult Index(string filter, int? page, int pageSize = 10)
        {
            int pageNumber = (page ?? 1);
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
                    TicketsCount = r.TicketsCount
                }).ToList(),
                Filter = filter,
                PageNumber = pageNumber,
                PagesCount = (int) Math.Ceiling(reservationService.GetAllReservations().Count / (double) pageSize),
                PageSize = pageSize
            };

            if(filter == "email") { 
                model.Reservations = model.Reservations.OrderBy(r => r.Email).ToList();
            }
            else if(filter == "emailReversed")
            {
                model.Reservations = model.Reservations.OrderByDescending(r => r.Email).ToList();
            }

            model.Reservations = model.Reservations.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

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
                    BusinessTicketsLeft = f.BusinessTicketsLeft,
                    TicketsLeft = f.TicketsLeft
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(ReservationCreateViewModel model)
        {
            Flight flight = flightService.GetFlightById(model.FlightId);
            if (model.TicketType == "Business" && flight.BusinessTicketsLeft < model.TicketCount)
            {
                model.ErrorMessage = "Not enough Business class tickets!";
                return View("Create", model);
            }
            else if (model.TicketType == "Regular" && flight.TicketsLeft < model.TicketCount)
            {
                model.ErrorMessage = "Not enough Regular class tickets!";
                return View("Create", model);
            }

            ReservationCreateViewModel resModel = new ReservationCreateViewModel() {
                Email = model.Email,
                FirstName = model.FirstName,
                SecondName = model.SecondName,
                LastName = model.LastName,
                SSN = model.SSN,
                PhoneNumber = model.PhoneNumber,
                Nationality = model.Nationality,
                TicketType = model.TicketType,
                TicketCount = model.TicketCount,
                BusinessTicketsLeft = flight.BusinessTicketsLeft,
                FlightId = flight.FlightID,
                TicketsLeft = flight.TicketsLeft
            };

            reservationService.CreateReservation(resModel);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Confirm(Guid id) { 
            if(reservationService.GetReservationById(id) == null) { 
                return RedirectToAction("NotFound");
            }

            reservationService.ConfirmReservation(id);

            Reservation reservation = reservationService.GetReservationById(id);

            ReservationConfirmViewModel model = new ReservationConfirmViewModel() { 
                DepartureCity = reservation.Flight.DepartureCity,
                DestinationCity = reservation.Flight.DestinationCity,
                Name = reservation.FirstName + " " + reservation.LastName
            };

            return View(model);
        }

        public IActionResult Delete(Guid id) {
            if (reservationService.GetReservationById(id).IsConfirmed) { 
                return View("CannotDelete");
            }
            reservationService.DeleteReservation(id);

            return View();
        }
    }
}