using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Data
{
    public class FlightUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [StringLength(10)]
        public string SSN { get; set; } // ЕГН
        public string Address { get; set; }
    }
}