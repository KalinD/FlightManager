using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Models
{
    public class FlightReservationViewModel
    {
        public string Name { get; set; }
        public string SSN { get; set; }
        public string PhoneNumber { get; set; }
        public string Nationality { get; set; }
        public string TicketType { get; set; }
        public string Email { get; set; }
    }
}
