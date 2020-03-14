using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Data
{
    public class Reservation
    {
        [Key]
        public Guid ReservationID { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string SSN { get; set; }
        public string PhoneNumber { get; set; }
        public string Nationality { get; set; }
        public string TicketType { get; set; }
        public int TicketsCount { get; set; }
        public string Email { get; set; }
        public Guid FlightID { get; set; }
        public Flight Flight { get; set; }
        public bool IsConfirmed { get; set; }
    }
}