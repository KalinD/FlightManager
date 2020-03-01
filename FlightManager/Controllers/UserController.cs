using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightManager.Models;
using FlightManager.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightManager.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService service)
        {
            userService = service;
        }

        [Authorize]
        public IActionResult Index()
        {
            UsersIndexViewModel model = new UsersIndexViewModel()
            {
                Users = userService.GetAllUsers().Select(u => new UserIndexViewModel
                {
                    Address = u.Address,
                    Name = u.FirstName + " " + u.LastName,
                    PhoneNumber = u.PhoneNumber,
                    SSN = u.SSN,
                    Username = u.UserName
                }).ToList()
            };

            return View(model);
    }
}
}