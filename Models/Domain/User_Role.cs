namespace SharkSpotterAPI.Models.Domain
{
    public class User_Role
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }

        // Navigational Properties
        public Role Role { get; set; }
        public User User { get; set; }
    }
}
