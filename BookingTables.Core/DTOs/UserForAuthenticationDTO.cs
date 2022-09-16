using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class UserForAuthenticationDTO
    {
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
