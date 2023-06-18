using Domain.DTOs;

namespace Domain.Interfaces.AppService
{
    public interface IAuthAppService
    {
        Task<AuthResponseDto> AutenticateAsync(AuthDto auth);
        Task UpdatePassword(int userId, UserDto user);
    }
}
