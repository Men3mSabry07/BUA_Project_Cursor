namespace BUA_project.Models
{
    public class FuelPrice
    {
        public int FuelPriceId { get; set; }

        public decimal PricePerLiter { get; set; }

        public DateTime UpdatedAt { get; set; }

        public int? UpdatedByUserId { get; set; }

        public User? UpdatedByUser { get; set; }
    }
}