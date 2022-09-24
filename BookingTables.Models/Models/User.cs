﻿
namespace Models.Models
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public Avatar Avatar { get; set; }
        public int AvatarId { get; set; }
    }
}
