using AutoMapper;
using Ecommerce.Abstruction.IServices;
using Ecommerce.Domain.Exceptions;
using Ecommerce.Persistance.Identity.Models;
using Ecommerce.Shared.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;

        public AuthenticationService(UserManager<ApplicationUser> userManager,IConfiguration configuration,IMapper mapper)
        {
            _user_manager_check: _ = userManager; // keep analyzer happy if needed
            _userManager = userManager;
            this.configuration = configuration;
            this.mapper = mapper;
        }

        public async Task<bool> checkEmailExistAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user is not  null;
        }

        public async Task<UserDto> GetCurrentUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email) ?? throw new UserNotFoundException(email);
            return new UserDto
            {
                Email = user.Email,
                DisplayName = user.DesplayNamr,
                Token = await GenerateJwtTokenAsync(user)
            };

        }

        public async Task<AddressDto> GettCurrentUserAddressAsync(string email)
        {
            var user = await _userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(e => e.Email == email);
            if (user.Address is not null)
            {
                return mapper.Map<Address, AddressDto>(user.Address);
            }
            else
            {
                throw new AddressNotFoundExeption(user.DesplayNamr);
            }
        }
        public async Task<AddressDto> UpdateUserAddressAsync(string email, AddressDto addressDto)
        {
            var user =  _userManager.Users.Include(u => u.Address).FirstOrDefault(e => e.Email == email)??throw new UserNotFoundException(email);
            if (user.Address is not null)
            {
                user.Address.Street = addressDto.Street;
                user.Address.City = addressDto.City;
                user.Address.Country = addressDto.Country;
                user.Address.FirstName = addressDto.FirstName;
                user.Address.LastName = addressDto.LastName;

            }
            else
            {
                user.Address = mapper.Map<AddressDto, Address>(addressDto);
            }
            await _userManager.UpdateAsync(user);
            return mapper.Map<Address, AddressDto>(user.Address);

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
                Token = await GenerateJwtTokenAsync(user)
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
                    Token = await GenerateJwtTokenAsync(user)
                };
            }
            else
            { 
                var errors=result.Errors.Select(e=>e.Description).ToList();
                throw new BadRequesException(errors);
            }

        }

        private async Task<string> GenerateJwtTokenAsync(ApplicationUser user)
        {
            // Implementation for generating JWT token
            var claims = new List<Claim>()
            {
                new (ClaimTypes.Email,user.Email),
                new (ClaimTypes.NameIdentifier,user.Id),
                new (ClaimTypes.Name,user.UserName)

            };
            var Roles = await _userManager.GetRolesAsync(user);
            foreach (var role in Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var securityKey = configuration.GetSection("JWTOptions")["secretKey"];
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken
                (
                  issuer: configuration.GetSection("JWTOptions")["Issuer"],
                  audience: configuration.GetSection("JWTOptions")["Audience"],
                  claims: claims,
                  expires: DateTime.Now.AddDays(2),
                    signingCredentials: creds


                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
