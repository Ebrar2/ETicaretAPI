using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Commands.User.LoginUser
{
    public class LoginUserCommandResponse
    {
        public string Message { get; set; }
        public bool Succeeded { get; set; } = false;
    }
    public class LoginUserSuccessCommandResponse:LoginUserCommandResponse
    {
        public string AccessToken { get; set; }
        public LoginUserSuccessCommandResponse()
        {
            Succeeded = true;
        }
    }
    public class LoginUserErrorCommandResponse : LoginUserCommandResponse
    {

    }
}
