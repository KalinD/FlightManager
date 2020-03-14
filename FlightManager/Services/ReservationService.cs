using FlightManager.Data;
using FlightManager.Services.Contracts;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Services
{
    public class ReservationService : IReservationService
    {
        private readonly FlightManagerDbContext dBContext;
        private readonly IEmailSender emailSender;

        public ReservationService(FlightManagerDbContext context, IEmailSender emailSender)
        {
            dBContext = context;
            this.emailSender = emailSender;
        }

        public Reservation ChangeTicketType(Reservation reservation)
        {
            dBContext.Reservations.Where(r => r.ReservationID == reservation.ReservationID).First().TicketType = reservation.TicketType == "Business" ? "Regular" : "Business";
            dBContext.SaveChanges();

            return reservation;
        }

        public List<Reservation> GetAllReservations()
        {
            return dBContext.Reservations.ToList();
        }

        public List<Reservation> GetAllReservationsForFlight(Flight flight)
        {
            return dBContext.Reservations.Where(r => r.FlightID == flight.FlightID).ToList();
        }

        public Reservation CreateReservation(string email, string firstName, string secondName, string lastName, string sSN, string phoneNumber, string nationality, string ticketType, int ticketCount, Flight flight)
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
                Flight = flight,
                IsConfirmed = false,
                TicketsCount = ticketCount,
                FlightID = flight.FlightID
            };

            Flight dbFlight = dBContext.Flights.Where(f => f.FlightID == flight.FlightID).First();
            if (ticketType == "Business")
            {
                dbFlight.BusinessTicketsLeft -= ticketCount;
            }
            else
            {
                dbFlight.TicketsLeft -= ticketCount;
            }

            dBContext.Reservations.Add(reservation);
            dBContext.SaveChanges();

            string msg = $@"Confirmation for flight from {dbFlight.DepartureCity} to {dbFlight.DestinationCity}
                            <a href={"https://localhost:44361"}/Reservation/Confirm?id={reservation.ReservationID}>Confirm</a>
                            <a href={"https://localhost:44361"}/Reservation/Delete?id={reservation.ReservationID}>Delete</a>";

            emailSender.SendEmailAsync(reservation.Email, "Reservation Confirmation", msg).GetAwaiter().GetResult();

            return reservation;
        }

        public Reservation ConfirmReservation(Guid id) { 
            Reservation reservation = dBContext.Reservations.Where(r => r.ReservationID == id).First();
            reservation.IsConfirmed = true;

            dBContext.Reservations.Update(reservation);
            dBContext.SaveChanges();

            return reservation;
        }

        public Reservation DeleteReservation(Guid id)
        {
            Reservation reservation = dBContext.Reservations.Where(r => r.ReservationID == id).First();
            
            if (reservation.IsConfirmed) { 
                return reservation;
            }

            dBContext.Reservations.Remove(reservation);
            dBContext.SaveChanges();

            return reservation;
        }

        public Reservation GetReservationById(Guid id)
        {
            return dBContext.Reservations.Where(r => r.ReservationID == id).First();
        }
    }
}
