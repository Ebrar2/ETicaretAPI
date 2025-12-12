using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.DTOs.User
{
    public class LoginWithRefreshTokenDTO
    {
        public string Message { get; set; }
        public bool Succeeded { get; set; } = false;
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
