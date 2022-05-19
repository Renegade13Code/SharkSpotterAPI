namespace SharkSpotterAPI.Models.DTO
{
    public class BeachDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Geolocation { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
