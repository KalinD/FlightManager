using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Models
{
    public class ReservationCreateViewModel
    {
        public List<ReservationFlightCreateViewModel> Flights { get; set; }
        public Guid FlightId { get; set; }
        public int TicketCount { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string SSN { get; set; }
        public string PhoneNumber { get; set; }
        public string Nationality { get; set; }
        public string TicketType { get; set; }
    }
}
