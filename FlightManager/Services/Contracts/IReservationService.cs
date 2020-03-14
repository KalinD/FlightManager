using FlightManager.Data;
using System;
using System.Collections.Generic;

namespace FlightManager.Services.Contracts
{
    public interface IReservationService
    {
        public Reservation CreateReservation(string email, string firstName, string secondName, string lastName, string sSN, string phoneNumber, string nationality, string ticketType, int ticketCount, Flight flight);
        public Reservation DeleteReservation(Guid id);
        public Reservation GetReservationById(Guid id);
        public List<Reservation> GetAllReservations();
        public List<Reservation> GetAllReservationsForFlight(Flight flight);
        public Reservation ConfirmReservation(Guid id);
        public Reservation ChangeTicketType(Reservation reservation);
    }
}
