using LOGIN.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOGIN.Models.Mappings
{
    public class UserMappings
    {
        public static UserResponseDto GetUserResponse(AppUser user)
        {
            return new UserResponseDto
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Id = user.Id
            };
        }

        public static AppUser GetUser(RegistrationRequest registration)
        {
            var user = new AppUser
            {
                Email = registration.Email,
                FirstName = registration.FirstName,
                LastName = registration.LastName,
                PhoneNumber = registration.PhoneNumber,
                UserName = string.IsNullOrWhiteSpace(registration.UserName) ? registration.Email : registration.UserName,
            };
            return user;
        }
    }
}
