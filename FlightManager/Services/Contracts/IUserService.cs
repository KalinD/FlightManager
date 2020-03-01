using FlightManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Services.Contracts
{
    public interface IUserService
    {
        public FlightUser CreateUser(string username, string password, string firstName, string lastName, string sSN, string address, string phoneNumber);
        public FlightUser DeleteUser(FlightUser user);
        public FlightUser GetUserByUsername(string username);
        public FlightUser GetUserByEmail(string email);
        public List<FlightUser> GetAllUsers();
        public FlightUser UpdateUsername(FlightUser user, string newUsername);
        public FlightUser UpdateAddress(FlightUser user, string newAddress);
        public FlightUser UpdatePhoneNumber(FlightUser user, string newPhoneNumber);
    }
}
