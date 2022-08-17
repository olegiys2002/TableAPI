using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class UserFormDTO
    {
        [Required(ErrorMessage = "Name is a required field")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Role is a required field")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Password is a required field")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is a required field")]
        public string Email { get; set; }
        public AvatarFormDTO AvatarFormDTO { get; set; }
    }
}
