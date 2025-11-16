using Ecommerce.Abstruction.IServices;
using Ecommerce.Domain.Exceptions;
using Ecommerce.Persistance.Identity.Models;
using Ecommerce.Shared.Dtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthenticationService(UserManager<ApplicationUser> userManager)
        {
            _user_manager_check: _ = userManager; // keep analyzer happy if needed
            _userManager = userManager;
        }

        public async Task<UserDto> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email) ?? throw new UserNotFoundException(dto.Email);

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!isPasswordValid)
            {
                throw new UnauthorizedAccessException("Invalid Email Or password.");
            }

            return new UserDto
            {
                Email = user.Email,
                DisplayName = user.DesplayNamr,
                Token = string.Empty
            };
        }

        public async Task<UserDto> RegisterAsync(RegisterDto dto)
        {
            var user = new ApplicationUser
            {
                DesplayNamr = dto.DisplayName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                UserName = dto.UserName
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                return new UserDto
                {
                    Email = user.Email,
                    DisplayName = user.DesplayNamr,
                    Token = string.Empty
                };
            }
            else
            { 
                var errors=result.Errors.Select(e=>e.Description).ToList();
                throw new BadRequesException(errors);
            }

        }
    }
}
