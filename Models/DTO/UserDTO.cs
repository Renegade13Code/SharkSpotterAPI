﻿namespace SharkSpotterAPI.Models.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        // Navigational properties
        // Leave as UserRoles?
        public List<RoleDTO> Roles { get; set; }
    }
}