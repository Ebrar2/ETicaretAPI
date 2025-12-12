using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Commands.User.LoginWithRefreshToken
{
    public class LoginWithRefreshTokenCommandRequest:IRequest<LoginWithRefreshTokenCommandResponse>
    {
        public string RefreshToken { get; set; }
    }
}
