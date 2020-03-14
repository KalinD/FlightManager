namespace FlightManager.Models
{
    public class UserDetailsViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SSN { get; set; }
        public string Address { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual bool EmailConfirmed { get; set; }
        public virtual string Email { get; set; }
        public virtual string UserName { get; set; }
    }
}
