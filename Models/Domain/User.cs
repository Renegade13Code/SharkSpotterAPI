namespace SharkSpotterAPI.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        // Navigational properties
        public List<User_Role> UserRoles { get; set; }
        public List<SharkStatus> SharkStatuses { get; set; }
    }
}
