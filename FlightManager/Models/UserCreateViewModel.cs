using System.ComponentModel.DataAnnotations;

namespace FlightManager.Models
{
    public class UserCreateViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Social Security Number")]
        public string SSN { get; set; }
        public string Address { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
