using LOGIN.Models.DTOs;
using System.Threading.Tasks;

namespace LOGIN.Core
{
    public interface IAuthentication
    {
        Task<UserResponseDto> Login(UserRequestDto requestDto);
        Task<UserResponseDto> Register(RegistrationRequest registration);
    }
}