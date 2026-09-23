namespace BUA_project.Models
{
    public class RouteEstimate
    {
        public int RouteEstimateId { get; set; }

        public string Origin { get; set; }

        public string Destination { get; set; }
                                            
        public double Distance { get; set; }

        public TimeSpan Duration { get; set; }

        public string ProviderSnapshot { get; set; }

        public DateTime CalculatedAt { get; set; }

        public Trip? Trip { get; set; }
    }
}