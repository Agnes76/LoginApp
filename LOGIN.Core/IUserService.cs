using LOGIN.Models.DTOs;
using System.Threading.Tasks;

namespace LOGIN.Core
{
    public interface IUserService
    {
        Task<bool> DeleteUser(string userId);
        Task<UserResponseDto> GetUser(string userId);
        Task<bool> UpdateUser(string userId, UpdateUserRequest updateRequest);
    }
}