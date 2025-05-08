using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dto_s.IdentityDto;

namespace Abstraction
{
    public interface IAuthenticationServices
    {
        //login
        //Take Email Password =>Token - Email - Display Name
        Task<UserDto> LoginAsync (LoginDto loginDto);



        //register
        //Take Email Password DisplayName UserName PhoneNumber =>Token - Email - Display Name
        Task<UserDto> registerAsync(RegisterDto registerDto);

    }
}
