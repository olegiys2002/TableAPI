using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
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
