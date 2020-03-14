﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FlightManager.Data;
using FlightManager.Models;
using FlightManager.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace FlightManager.Controllers
{
    public class FlightController : Controller
    {
        private readonly IFlightsService flightService;
        private readonly IUserService userService;
        private readonly IReservationService reservationService;


        public FlightController(IFlightsService flightService, IUserService userService, IReservationService reservationService)
        {
            this.flightService = flightService;
            this.userService = userService;
            this.reservationService = reservationService;
        }

        [Authorize]
        public IActionResult Index(string searchString, int? page, int pageSize = 10)
        {
            int pageNumber = (page ?? 1);
            FlightsIndexViewModel model = new FlightsIndexViewModel()
            {
                Flights = flightService.GetAllFlights().Select(f => new FlightIndexViewModel()
                {
                    FlightId = f.FlightID,
                    TravelTime = f.ArrivalTime.Subtract(f.DepartureTime),
                    BusinessClassCapacity = f.BusinessClassCapacity,
                    CaptainName = f.CaptainName,
                    DepartureCity = f.DepartureCity,
                    DepartureTime = f.DepartureTime,
                    DestinationCity = f.DestinationCity,
                    PlaneCapacity = f.PlaneCapacity,
                    PlaneID = f.PlaneID,
                    PlaneType = f.PlaneType
                }).ToList(),
                PageNumber = pageNumber,
                PageSize = pageSize,
                PagesCount = (int) Math.Ceiling(flightService.GetAllFlights().Count / (double) pageSize)
            };

            if (!String.IsNullOrEmpty(searchString))
            {
                model.Flights = model.Flights.Where(f => f.DepartureCity.Contains(searchString) || f.DestinationCity.Contains(searchString)).ToList();
            }

            model.Flights = model.Flights.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid id)
        {
            flightService.DeleteFlight(flightService.GetFlightById(id));

            return RedirectToAction("Index");
        }

        public IActionResult Details(Guid id, int? page, int pageSize = 10)
        {
            int pageNumber = (page ?? 1);
            Flight flight = flightService.GetFlightById(id);
            FlightDetailsViewModel model = new FlightDetailsViewModel()
            {
                FlightId = flight.FlightID,
                BusinessClassCapacity = flight.BusinessClassCapacity,
                CaptainName = flight.CaptainName,
                DepartureCity = flight.DepartureCity,
                DepartureTime = flight.DepartureTime,
                DestinationCity = flight.DestinationCity,
                FlightDuration = flight.ArrivalTime.Subtract(flight.DepartureTime),
                PlaneCapacity = flight.PlaneCapacity,
                PlaneType = flight.PlaneType,
                BusinessTicketsLeft = flight.BusinessTicketsLeft,
                TicketsLeft = flight.TicketsLeft,
                Reservations = reservationService.GetAllReservationsForFlight(flight).Select(r => new FlightReservationViewModel()
                {
                    Email = r.Email,
                    Name = r.FirstName + " " + r.SecondName + " " + r.LastName,
                    Nationality = r.Nationality,
                    PhoneNumber = r.PhoneNumber,
                    SSN = r.SSN,
                    TicketType = r.TicketType,
                    TicketsCount = r.TicketsCount
                }).ToList(),
                PageNumber = pageNumber,
                PageSize = pageSize,
                PagesCount = (int) Math.Ceiling(reservationService.GetAllReservationsForFlight(flight).Count / (double)pageSize)
            };

            model.Reservations = model.Reservations.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return View(model);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(Guid id)
        {
            Flight flight = flightService.GetFlightById(id);
            EditFlightViewModel model = new EditFlightViewModel()
            {
                ArrivalTime = flight.ArrivalTime,
                BusinessClassCapacity = flight.BusinessClassCapacity,
                CaptainName = flight.CaptainName,
                DepartureCity = flight.DepartureCity,
                DepartureTime = flight.DepartureTime,
                DestinationCity = flight.DestinationCity,
                PlaneCapacity = flight.PlaneCapacity,
                PlaneID = flight.PlaneID,
                PlaneType = flight.PlaneType
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Guid id, string destinationCity, string departureCity, DateTime departureTime, DateTime arrivalTime,
            string planeType, string planeID, string captainName, int planeCapacity, int businessClassCapacity)
        {
            Flight flight = new Flight()
            {
                ArrivalTime = arrivalTime,
                BusinessClassCapacity = businessClassCapacity,
                CaptainName = captainName,
                DepartureCity = departureCity,
                DepartureTime = departureTime,
                DestinationCity = destinationCity,
                PlaneCapacity = planeCapacity,
                PlaneID = planeID,
                PlaneType = planeType
            };
            flightService.UpdateFlight(id, flight);

            return RedirectToAction("Details");
        }


        [HttpPost]
        [Authorize]
        public IActionResult Create(string destinationCity, string departureCity, DateTime departureTime, DateTime arrivalTime,
            string planeType, string planeID, string captainName, int planeCapacity, int businessClassCapacity)
        {
            if (arrivalTime < departureTime)
            {
                return RedirectToAction("Create");
            }
            flightService.CreateFlight(destinationCity, departureCity, departureTime, arrivalTime, planeType, planeID, captainName, planeCapacity, businessClassCapacity);
            return RedirectToAction("Index");
        }
    }
}