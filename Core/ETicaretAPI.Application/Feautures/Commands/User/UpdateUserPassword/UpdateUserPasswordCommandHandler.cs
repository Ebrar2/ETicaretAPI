using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Commands.User.UpdateUserPassword
{
    public class UpdateUserPasswordCommandHandler : IRequestHandler<UpdateUserPasswordCommandRequest, UpdateUserPasswordCommandResponse>
    {
        readonly IUserService userService;

        public UpdateUserPasswordCommandHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<UpdateUserPasswordCommandResponse> Handle(UpdateUserPasswordCommandRequest request, CancellationToken cancellationToken)
        {
            await userService.UpdateUserPasswordAsync(request.UserId, request.ResetToken, request.Password);
            return new();
        }
    }
}
