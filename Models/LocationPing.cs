namespace BUA_project.Models
{
    public class LocationPing
    {
        public int LocationPingId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Timestamp { get; set; }

        // Foreign Key + Navigation
        public int TripId { get; set; }
        public Trip Trip { get; set; }
    }
}