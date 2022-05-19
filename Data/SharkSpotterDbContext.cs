using Microsoft.EntityFrameworkCore;
using SharkSpotterAPI.Models;
using SharkSpotterAPI.Models.Domain;

namespace SharkSpotterAPI.Data
{
    public class SharkSpotterDbContext : DbContext
    {
        public SharkSpotterDbContext( DbContextOptions<SharkSpotterDbContext> options) : base(options)
        {

        }

        public DbSet<Beach> Beaches { get; set; }
        public DbSet<SharkStatus> SharkStatuses { get; set; }
        public DbSet<Flag> Flags { get; set; }
    }
}
