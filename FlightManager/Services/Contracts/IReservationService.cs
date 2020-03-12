using FlightManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Services.Contracts
{
    public interface IReservationService
    {
        public Reservation CreateReservation(string email, string firstName, string secondName, string lastName, string sSN, string phoneNumber, string nationality, string ticketType, Flight flight);
        public Reservation DeleteReservation(Reservation reservation);
        public Reservation GetReservation(string firstName, string secondName, string lastName);
        public List<Reservation> GetAllReservations();
        public List<Reservation> GetAllReservationsForFlight(Flight flight);
        public Reservation UpdatePhoneNumber(Reservation reservation, string newPhoneNumber);
        public Reservation ChangeTicketType(Reservation reservation);
    }
}
