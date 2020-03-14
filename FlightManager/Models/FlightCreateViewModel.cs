﻿using System;

namespace FlightManager.Models
{
    public class FlightCreateViewModel
    {
        public string DestinationCity { get; set; }
        public string DepartureCity { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string PlaneType { get; set; }
        public string PlaneID { get; set; }
        public string CaptainName { get; set; }
        public int PlaneCapacity { get; set; }
        public int BusinessClassCapacity { get; set; }
    }
}
