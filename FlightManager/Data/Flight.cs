using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Data
{
    public class Flight
    {
        [Key]
        public Guid FlightID { get; set; }
        public string DestinationCity { get; set; }
        public string DepartureCity { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string PlaneType { get; set; }
        public string PlaneID { get; set; }
        public string CaptainName { get; set; }
        public int PlaneCapacity { get; set; }
        public int TicketsLeft { get; set; }
        public int BusinessClassCapacity { get; set; }
        public int BusinessTicketsLeft { get; set; }
    }
}