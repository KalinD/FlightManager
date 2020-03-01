using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Models
{
    public class ReservationFlightCreateViewModel
    {
        public Guid FlightID { get; set; }
        public DateTime DepartureTime { get; set; }
        public string DepartureCity { get; set; }
        public string DestinationCity { get; set; }
    }
}
