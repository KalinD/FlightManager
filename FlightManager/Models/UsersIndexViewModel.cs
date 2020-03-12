using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Models
{
    public class UsersIndexViewModel
    {
        public List<UserIndexViewModel> Users;
        public int PageCount;
        public int PageNumber;
    }
}
