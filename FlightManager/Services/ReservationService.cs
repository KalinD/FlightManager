using FlightManager.Data;
using FlightManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Services
{
    public class ReservationService : IReservationService
    {
        private readonly FlightManagerDbContext dBContext;

        public ReservationService(FlightManagerDbContext context)
        {
            dBContext = context;
        }

        public Reservation ChangeTicketType(Reservation reservation)
        {
            reservation.TicketType = reservation.TicketType == "Business" ? "Regular" : "Business";
            dBContext.SaveChanges();

            return reservation;
        }

        public List<Reservation> GetAllReservations() { 
            return dBContext.Reservations.ToList();
        }

        public Reservation CreateReservation(string email, string firstName, string secondName, string lastName, string sSN, string phoneNumber, string nationality, string ticketType, Flight flight)
        {
            Reservation reservation = new Reservation()
            {
                Email = email,
                FirstName = firstName,
                SecondName = secondName,
                LastName = lastName,
                SSN = sSN,
                PhoneNumber = phoneNumber,
                Nationality = nationality,
                TicketType = ticketType,
                Flight = flight
            };

            dBContext.Reservations.Add(reservation);
            dBContext.SaveChanges();

            return reservation;
        }

        public Reservation DeleteReservation(Reservation reservation)
        {
            dBContext.Reservations.Remove(reservation);
            dBContext.SaveChanges();

            return reservation;
        }

        public Reservation GetReservation(string firstName, string lastName, string SSN)
        {
            return dBContext.Reservations.Where(r => r.FirstName == firstName && r.LastName == lastName && r.SSN == SSN).First();
        }

        public Reservation UpdatePhoneNumber(Reservation reservation, string newPhoneNumber)
        {
            reservation.PhoneNumber = newPhoneNumber;
            dBContext.SaveChanges();

            return reservation;
        }
    }
}
