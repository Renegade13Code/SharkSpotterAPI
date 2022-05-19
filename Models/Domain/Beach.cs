using SharkSpotterAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace SharkSpotterAPI.Models
{
    public class Beach
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Geolocation { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        //Navigational properties
        public IEnumerable<SharkStatus> StatusHistory { get; set; }
    }
}
