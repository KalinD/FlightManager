using FlightManager.Data;
using FlightManager.Models;
using FlightManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Services
{
    public class UserService : IUserService
    {
        private readonly FlightManagerDbContext dBContext;

        public UserService(FlightManagerDbContext context)
        {
            dBContext = context;
        }

        public FlightUser CreateUser(string username, string password, string firstName, string lastName, string sSN, string address, string phoneNumber)
        {
            FlightUser user = new FlightUser()
            {
                UserName = username,
                FirstName = firstName,
                LastName = lastName,
                SSN = sSN,
                Address = address,
                PhoneNumber = phoneNumber
            };

            dBContext.Users.Add(user);
            dBContext.SaveChanges();

            return user;
        }

        public FlightUser GetUserById(string id) { 
            return dBContext.Users.Where(u => u.Id == id).First();
        }

        public FlightUser DeleteUser(FlightUser user)
        {
            dBContext.Users.Remove(user);
            dBContext.SaveChanges();

            return user;
        }

        public List<FlightUser> GetAllUsers()
        {
            return dBContext.Users.ToList();
        }

        public FlightUser GetUserByUsername(string username)
        {
            return dBContext.Users.Where(u => u.UserName == username).First();
        }

        public FlightUser GetUserByEmail(string email)
        {
            return dBContext.Users.Where(u => u.Email == email).First();
        }

        public FlightUser UpdateUser(string id, UserEditViewModel user)
        {
            FlightUser dbUser = dBContext.Users.Where(u => u.Id == id).First();

            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;
            dbUser.Address = user.Address;
            dbUser.UserName = user.UserName;

            dBContext.Users.Update(dbUser);

            dBContext.SaveChanges();

            return dbUser;
        }

        public int GetUsersCount()
        {
            return dBContext.Users.Count();
        }
    }
}
