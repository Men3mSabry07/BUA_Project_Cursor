namespace BUA_project.Models
{
    public class FuelEstimate
    {
        public int FuelEstimateId { get; set; }

        public double PredictedFuel { get; set; }

        public decimal FuelPricePerLiter { get; set; }

        public decimal EstimatedCost { get; set; }

        public string? Model { get; set; }

        public string? Features { get; set; }

        // Foreign Key + Navigation (1:1 with Reservation)
        public int ReservationId { get; set; }

        public Reservation Reservation { get; set; }
    }
}