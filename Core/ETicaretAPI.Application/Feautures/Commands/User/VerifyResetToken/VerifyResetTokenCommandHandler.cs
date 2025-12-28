using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Commands.User.VerifyResetToken
{
    public class VerifyResetTokenCommandHandler : IRequestHandler<VerifyResetTokenCommandRequest, VerifyResetTokenCommandResponse>
    {
        readonly IAuthService authService;

        public VerifyResetTokenCommandHandler(IAuthService authService)
        {
            this.authService = authService;
        }

        public async Task<VerifyResetTokenCommandResponse> Handle(VerifyResetTokenCommandRequest request, CancellationToken cancellationToken)
        {
            bool result= await authService.VerifyResetTokenAsync(request.UserId, request.ResetToken);
            return new() { IsVerified = result };
        }
    }
}
