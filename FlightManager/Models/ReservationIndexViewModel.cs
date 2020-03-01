﻿using FlightManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Models
{
    public class ReservationIndexViewModel
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string TicketType { get; set; }
        public string Email { get; set; }
        public string DestinationCity { get; set; }
        public string DepartureCity { get; set; }
        public DateTime DepartureTime { get; set; }
    }
}
