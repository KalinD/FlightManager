using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Models
{
    public class FlightDetailsViewModel
    {
        public Guid FlightId { get; set; }
        public string DestinationCity { get; set; }
        public string DepartureCity { get; set; }
        public DateTime DepartureTime { get; set; }
        public TimeSpan FlightDuration { get; set; }
        public string PlaneType { get; set; }
        public string CaptainName { get; set; }
        public int PlaneCapacity { get; set; }
        public int BusinessClassCapacity { get; set; }
        public List<FlightReservationViewModel> Reservations { get; set; }
    }
}
