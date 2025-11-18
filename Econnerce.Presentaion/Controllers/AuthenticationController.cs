using Ecommerce.Abstruction.IServices;
using Ecommerce.Persistance.Identity.Models;
using Ecommerce.Shared.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Econnerce.Presentaion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(IServiceManger serviceManger) : ControllerBase
    {


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto loginDto)
        {
            var user = await serviceManger.AuthenticationService.LoginAsync(loginDto);
            return Ok(user);

        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto registerDto)
        {
            var user = await serviceManger.AuthenticationService.RegisterAsync(registerDto);
            return Ok(user);

        }
        [Authorize]
        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmail([FromQuery] string email)
        {
            var isEmailExist = await serviceManger.AuthenticationService.checkEmailExistAsync(email);
            return Ok(isEmailExist);
        }
        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await serviceManger.AuthenticationService.GetCurrentUserAsync(email);
            return Ok(user);
        }
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        {
            var email=User.FindFirstValue(ClaimTypes.Email);
            var Address=await serviceManger.AuthenticationService.GettCurrentUserAddressAsync(email);
            return Ok(Address);
        }

        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress([FromBody] AddressDto addressDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var Address = await serviceManger.AuthenticationService.UpdateUserAddressAsync(email,addressDto);
            return Ok(Address);
        }

    }
}
