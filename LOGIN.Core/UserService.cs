using LOGIN.Models;
using LOGIN.Models.DTOs;
using LOGIN.Models.Mappings;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOGIN.Core
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> UpdateUser(string userId, UpdateUserRequest updateRequest)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.FirstName = string.IsNullOrWhiteSpace(updateRequest.FirstName) ? user.FirstName : updateRequest.FirstName;
                user.LastName = string.IsNullOrWhiteSpace(updateRequest.LastName) ? user.LastName : updateRequest.LastName;
                user.PhoneNumber = string.IsNullOrWhiteSpace(updateRequest.PhoneNumber) ? user.PhoneNumber : updateRequest.PhoneNumber;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return true;
                }

                string errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += error.Description + Environment.NewLine;
                }
                throw new MissingMemberException(errors);
            }
            throw new ArgumentException("User not found");
        }

        public async Task<bool> DeleteUser(string userId)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return true;
                }
                string errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += error.Description + Environment.NewLine;
                }
                throw new MissingMemberException(errors);
            }
            throw new ArgumentException("User not found");
        }

        public async Task<UserResponseDto> GetUser(string userId)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                return UserMappings.GetUserResponse(user);
            }
            throw new ArgumentException("User not found");
        }
    }
}
