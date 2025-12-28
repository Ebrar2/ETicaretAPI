using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Commands.User.ResetPassword
{
    public class ResetPasswordComandHandler : IRequestHandler<ResetPasswordComandRequest, ResetPasswordComandResponse>
    {
        readonly IAuthService authService;

        public ResetPasswordComandHandler(IAuthService authService)
        {
            this.authService = authService;
        }

        public async Task<ResetPasswordComandResponse> Handle(ResetPasswordComandRequest request, CancellationToken cancellationToken)
        {
            await authService.ResetPasswordAsync(request.Email);
            return new();
        }
    }
}
