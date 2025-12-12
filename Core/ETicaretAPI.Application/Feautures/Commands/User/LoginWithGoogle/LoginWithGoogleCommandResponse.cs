using ETicaretAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Commands.User.LoginWithGoogle
{
    public class LoginWithGoogleCommandResponse
    {
        public string Message { get; set; }
        public  bool Succeeded { get; set; } = false;
    }
    public class LoginWithCommanSuccessResponse:LoginWithGoogleCommandResponse
    {
        public LoginWithCommanSuccessResponse()
        {
            Succeeded = true;
        }

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
    public class LoginWithCommanErrorResponse : LoginWithGoogleCommandResponse
    {

    }


}
