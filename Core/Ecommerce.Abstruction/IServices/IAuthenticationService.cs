using Ecommerce.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Abstruction.IServices
{
    public interface IAuthenticationService
    {
        public Task<UserDto> LoginAsync(LoginDto dto);
        public Task<UserDto> RegisterAsync(RegisterDto dto);

        public Task<bool> checkEmailExistAsync(string email);
        public Task<AddressDto> GettCurrentUserAddressAsync(string email);

        public Task<AddressDto> UpdateUserAddressAsync(string email, AddressDto addressDto);

        public Task<UserDto> GetCurrentUserAsync(string email);




    }
}
