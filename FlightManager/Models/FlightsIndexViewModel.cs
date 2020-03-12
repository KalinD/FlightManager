using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Models
{
    public class FlightsIndexViewModel
    {
        public List<FlightIndexViewModel> Flights { get; set; }
        public int PageCount;
        public int PageNumber;
        public string SearchString;
    }
}
