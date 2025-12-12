using ETicaretAPI.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> LoginAsync(LoginDTO loginDTO);
        Task<LoginWithGoogleResponseDTO> GoogleLoginAsync(LoginWithGoogleDTO loginWithGoogleDTO);
        Task<LoginWithRefreshTokenDTO> RefreshLoginAsync(string refreshToken);
    }
}
