using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightManager.Data;
using FlightManager.Models;
using FlightManager.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

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
        public IActionResult Index(int? page, int pageSize = 10)
        {
            int pageNumber = (page ?? 1);
            UsersIndexViewModel model = new UsersIndexViewModel()
            {
                Users = userService.GetAllUsers().Select(u => new UserIndexViewModel
                {
                    UserId = u.Id,
                    Address = u.Address,
                    Name = u.FirstName + " " + u.LastName,
                    PhoneNumber = u.PhoneNumber,
                    SSN = u.SSN,
                    Username = u.UserName
                }).ToList()
            };
            return View(model.Users.ToPagedList(pageNumber, pageSize));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string id) { 
            userService.DeleteUser(userService.GetUserById(id));

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(string id)
        {
            FlightUser user = userService.GetUserById(id);
            UserEditViewModel model = new UserEditViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(string id, string firstName, string lastName, string address, string username, string phoneNumber)
        {
            UserEditViewModel user = new UserEditViewModel(){ 
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                UserName = username,
                PhoneNumber = phoneNumber
            };

            userService.UpdateUser(id, user);

            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Details(string id) {
            FlightUser user = userService.GetUserById(id);
            UserDetailsViewModel model = new UserDetailsViewModel() { 
                Id = user.Id,
                Address = user.Address,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                Name = user.FirstName + " " + user.LastName,
                PhoneNumber = user.PhoneNumber,
                SSN = user.SSN,
                UserName = user.UserName
            };

            return View(model);
        }
    }
}