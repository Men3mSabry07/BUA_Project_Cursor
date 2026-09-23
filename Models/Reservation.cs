namespace BUA_project.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public int Passengers { get; set; }
        public double Load { get; set; }
        public string Purpose { get; set; }
        public string Status { get; set; }

        // Foreign Keys + Navigations
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }                 // select

        public int UserId { get; set; }
        public User User { get; set; }                       // Relationship

        public int? DriverId { get; set; }
        public Driver? Driver { get; set; }                  // Reserv

        public FuelEstimate? FuelEstimate { get; set; }      // has (1:1)

        public Trip? Trip { get; set; }                      // has (1:1)
    }
}
