using Microsoft.EntityFrameworkCore;

namespace SharkSpotterAPI.Models.Domain
{
    public class Flag : ModelBuilder
    {
        public Guid Id { get; set; }
        public string Color { get; set; }

        //Navigational properties
        public IEnumerable<SharkStatus> sharkStatuses { get; set; }
    }
}
