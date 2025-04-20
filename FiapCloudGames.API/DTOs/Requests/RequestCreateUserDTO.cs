﻿namespace FiapCloudGames.API.DTOs.Requests
{
    public class RequestCreateUserDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Guid RoleId { get; set; }
    }
}
