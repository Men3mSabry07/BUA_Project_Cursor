namespace BUA_project.DTOs
{
    public class FuelPredictionRequest
    {
        public float Distance_km { get; set; }

        public string Vehicle_Type { get; set; }

        public int Passengers { get; set; }

        public float Nominal_L_per_100km { get; set; }

        public float Duration_min { get; set; }
    }
}