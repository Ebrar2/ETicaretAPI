using ETicaretAPI.Application.DTOs.User;
using ETicaretAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponseDTO> CreateUser(CreateUserDTO createUserDTO);
        Task UpdateRefreshToken(string refreshToken,DateTime accessTokenDate,int refreshTokenDate,AppUser user);
    }
}
