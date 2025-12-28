using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Commands.User.UpdateUserPassword
{
   public class UpdateUserPasswordCommandRequest:IRequest<UpdateUserPasswordCommandResponse>
    {
        public string UserId { get; set; }
        public string ResetToken { get; set; }
        public string Password { get; set; }
    }
}
