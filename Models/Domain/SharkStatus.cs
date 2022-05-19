using Microsoft.EntityFrameworkCore;

namespace SharkSpotterAPI.Models.Domain
{
    public class SharkStatus : ModelBuilder
    {
        public Guid Id { get; set; }
        public Guid FlagId { get; set; }
        public Guid BeachId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End{ get; set; }

        // Navigational properties
        public Beach Beach { get; set; }
        public Flag Flag { get; set; }
    }
}
