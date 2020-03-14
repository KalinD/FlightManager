using System.Collections.Generic;

namespace FlightManager.Models
{
    public class UsersIndexViewModel
    {
        public List<UserIndexViewModel> Users { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int PagesCount { get; set; }
        public string Filter { get; set; }
    }
}
