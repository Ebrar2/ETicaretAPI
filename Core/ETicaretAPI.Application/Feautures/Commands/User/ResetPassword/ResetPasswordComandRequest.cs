using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Commands.User.ResetPassword
{
   public class ResetPasswordComandRequest:IRequest<ResetPasswordComandResponse>
    {
        public string Email { get; set; }
    }
}
