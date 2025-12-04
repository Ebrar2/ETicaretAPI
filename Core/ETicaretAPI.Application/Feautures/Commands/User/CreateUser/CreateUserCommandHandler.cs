using ETicaretAPI.Application.Exceptions.User;
using ETicaretAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Feautures.Commands.User.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly UserManager<AppUser> userManager;

        public CreateUserCommandHandler(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
           IdentityResult result=await userManager.CreateAsync(new AppUser()
            {
                Id=Guid.NewGuid().ToString(),
                UserName = request.UserName,
                NameSurname = request.NameSurname,
                Email = request.Email,

            },request.Password);
            CreateUserCommandResponse response = new() { Succeeded = result.Succeeded };
            if (result.Succeeded)
                response.Message = "Kullanıcı başarıyla eklendi";
            else
            {
                foreach (var error in result.Errors)
                    response.Message += error.Code.ToString()+"-"+ error.Description;
            }
            return response;
        }
    }
}
