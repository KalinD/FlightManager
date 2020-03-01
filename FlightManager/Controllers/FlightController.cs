using System;
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

namespace FlightManager.Controllers
{
    public class FlightController : Controller
    {
        private readonly IFlightsService flightService;
        private readonly IUserService userService;


        public FlightController(IFlightsService flightService, IUserService userService)
        {
            this.flightService = flightService;
            this.userService = userService;
        }

        [Authorize]
        public IActionResult Index()
        {
            FlightsIndexViewModel model = new FlightsIndexViewModel()
            {
                Flights = flightService.GetAllFlights().Select(f => new FlightIndexViewModel()
                {
                    TravelTime = f.ArrivalTime.Subtract(f.DepartureTime),
                    BusinessClassCapacity = f.BusinessClassCapacity,
                    CaptainName = f.CaptainName,
                    DepartureCity = f.DepartureCity,
                    DepartureTime = f.DepartureTime,
                    DestinationCity = f.DestinationCity,
                    PlaneCapacity = f.PlaneCapacity,
                    PlaneID = f.PlaneID,
                    PlaneType = f.PlaneType
                }).ToList()
            };

            return View(model);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(string destinationCity, string departureCity, DateTime departureTime, DateTime arrivalTime,
            string planeType, string planeID, string captainName, int planeCapacity, int businessClassCapacity)
        {
            if(arrivalTime < departureTime) { 
                return RedirectToAction("Create");
            }
            flightService.CreateFlight(destinationCity, departureCity, departureTime, arrivalTime, planeType, planeID, captainName, planeCapacity, businessClassCapacity);
            return RedirectToAction("Index");
        }
    }
}