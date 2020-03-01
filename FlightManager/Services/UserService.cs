using FlightManager.Data;
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
                PhoneNumber = phoneNumber,
                IsAdmin = dBContext.Users.ToList().Count == 0
            };

            dBContext.Users.Add(user);
            dBContext.SaveChanges();

            return user;
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

        public FlightUser UpdateAddress(FlightUser user, string newAddress)
        {
            user.Address = newAddress;
            dBContext.SaveChanges();

            return user;
        }

        public FlightUser UpdatePhoneNumber(FlightUser user, string newPhoneNumber)
        {
            user.PhoneNumber = newPhoneNumber;
            dBContext.SaveChanges();

            return user;
        }

        public FlightUser UpdateUsername(FlightUser user, string newUsername)
        {
            user.UserName = newUsername;
            dBContext.SaveChanges();

            return user;
        }
    }
}
