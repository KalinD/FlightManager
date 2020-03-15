using FlightManager.Data;
using FlightManager.Models;
using System;
using System.Collections.Generic;

namespace FlightManager.Services.Contracts
{
    public interface IReservationService
    {
        public Reservation CreateReservation(ReservationCreateViewModel model);
        public Reservation DeleteReservation(Guid id);
        public Reservation GetReservationById(Guid id);
        public List<Reservation> GetAllReservations();
        public List<Reservation> GetAllReservationsForFlight(Flight flight);
        public Reservation ConfirmReservation(Guid id);
        public Reservation ChangeTicketType(Reservation reservation);
    }
}
