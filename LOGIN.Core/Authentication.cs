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
    public class Authentication : IAuthentication
    {
        private readonly UserManager<AppUser> _useManager;
        private readonly ITokenGenerator _tokenGenerator;
        public Authentication(UserManager<AppUser> userManager, ITokenGenerator token)
        {
            _useManager = userManager;
            _tokenGenerator = token;
        }

        public async Task<UserResponseDto> Login(UserRequestDto requestDto)
        {
            AppUser user = await _useManager.FindByEmailAsync(requestDto.Email);
            if (user != null)
            {
                if (await _useManager.CheckPasswordAsync(user, requestDto.Password))
                {
                    var response = UserMappings.GetUserResponse(user);
                    response.Token = await _tokenGenerator.GenerateToken(user);

                    return response;    
                }
                throw new AccessViolationException("Invalid credentials");
            }
            throw new AccessViolationException("Invalid credentials");
        }

        public async Task<UserResponseDto> Register(RegistrationRequest registration)
        {
            AppUser user = UserMappings.GetUser(registration);

            IdentityResult result = await _useManager.CreateAsync(user, registration.Password);

            if (result.Succeeded)
            {
                return UserMappings.GetUserResponse(user);
            }

            string errors = string.Empty;
            foreach (var error in result.Errors)
            {
                errors += error.Description + Environment.NewLine;
            }
            throw new MissingFieldException(errors);
        }
    }
}
