using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Commands.User.LoginWithRefreshToken
{
    public class LoginWithRefreshTokenCommandResponse
    {
        public string Message { get; set; }
        public bool Succeeded { get; set; } = false;
    }
    public class LoginWithRefreshTokenCommandSuccessResponse: LoginWithRefreshTokenCommandResponse
    {
        public LoginWithRefreshTokenCommandSuccessResponse()
        {
            Succeeded = true;
        }

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
    public class LoginWithRefreshTokenCommandErrorResponse : LoginWithRefreshTokenCommandResponse
    {
        
    }
}
