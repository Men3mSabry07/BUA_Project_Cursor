namespace BUA_project.Models
{
    public class Driver
    {
        public int DriverId { get; set; }
        public string Name { get; set; }
        public string LicenseNumber { get; set; }
        public string QualificationStatus { get; set; }
        public DateTime QualificationValidUntil { get; set; }

        // Foreign Key + Navigation (Is relationship)
        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
