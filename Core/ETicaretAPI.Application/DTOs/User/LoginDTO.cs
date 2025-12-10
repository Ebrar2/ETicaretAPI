using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.DTOs.User
{
    public class LoginDTO
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
    public class LoginResponseDTO
    {
        public string Message { get; set; }
        public bool Succeeded { get; set; } = false;
        public string AccessToken { get; set; }

    }
}
