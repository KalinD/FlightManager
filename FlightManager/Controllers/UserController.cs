using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightManager.Data;
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
        public IActionResult Index(int? page, string filter, int pageSize = 10)
        {
            int pageNumber = (page ?? 1);
            UsersIndexViewModel model = new UsersIndexViewModel()
            {
                Users = userService.GetAllUsers().Select(u => new UserIndexViewModel
                {
                    UserId = u.Id,
                    Address = u.Address,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    SSN = u.SSN,
                    Username = u.UserName
                }).ToList(),
                Filter = filter,
                PageNumber = pageNumber,
                PageSize = pageSize,
                PagesCount = (int)(Math.Ceiling(userService.GetUsersCount() / (double)pageSize))
            };

            switch (filter)
            {
                case "email":
                    model.Users = model.Users.OrderBy(u => u.Email).ToList();
                    break;
                case "emailReversed":
                    model.Users = model.Users.OrderByDescending(u => u.Email).ToList();
                    break;
                case "username":
                    model.Users = model.Users.OrderBy(u => u.Username).ToList();
                    break;
                case "usernameReversed":
                    model.Users = model.Users.OrderByDescending(u => u.Username).ToList();
                    break;
                case "firstName":
                    model.Users = model.Users.OrderBy(u => u.FirstName).ToList();
                    break;
                case "firstNameReversed":
                    model.Users = model.Users.OrderByDescending(u => u.FirstName).ToList();
                    break;
                case "lastName":
                    model.Users = model.Users.OrderBy(u => u.LastName).ToList();
                    break;
                case "lastNameReversed":
                    model.Users = model.Users.OrderByDescending(u => u.LastName).ToList();
                    break;
            }

            model.Users = model.Users.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return View(model);
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
        public IActionResult Edit(string id, UserEditViewModel model)
        {
            userService.UpdateUser(id, model);

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