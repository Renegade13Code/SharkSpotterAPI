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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Liskov says not to remove this? udemy says we should?
            base.OnModelCreating(modelBuilder);

            // Define relationship between User_Roles with Role. 
            // Tells EF that one Role is associated with many entries in the User_Role table
            // And specify that RoleId is a foreign key'
            modelBuilder.Entity<User_Role>()
                .HasOne(x => x.Role)
                .WithMany(y => y.UserRoles)
                .HasForeignKey(x => x.RoleId);

            // Define relationship between User_Roles with User. 
            // Tells EF that one User is associated with many entries in the User_Role table
            // And specify that UserId is a foreign key'
            modelBuilder.Entity<User_Role>()
                .HasOne(x => x.User)
                .WithMany(y => y.UserRoles)
                .HasForeignKey(x => x.UserId);
        }

        public DbSet<Beach> Beaches { get; set; }
        public DbSet<SharkStatus> SharkStatuses { get; set; }
        public DbSet<Flag> Flags { get; set; }

        // User related roles
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User_Role> UserRoles { get; set; }
    }
}
