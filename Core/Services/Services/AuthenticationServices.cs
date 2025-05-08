using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Exceptions;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Shared.Dto_s.IdentityDto;

namespace Abstraction
{
    public class AuthenticationServices (UserManager<ApplicationUser> userManager ): IAuthenticationServices
    {
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            //Check If Email Is Exist
            var user =await userManager.FindByEmailAsync(loginDto.Email);

            if (user is null) throw new UserNotFoundException(loginDto.Email);

            //Check Passwoed
            var IsPasswordValid = await userManager.CheckPasswordAsync(user, loginDto.Password);
            if(IsPasswordValid)
            {
                return new UserDto()
                {
                    DisplayName = user.DisplayName,
                    Email = loginDto.Email,
                    Token = createTokenAsync(user)
                };
            }
            else
            {
                throw new UnauthorizedException();
            }
        }

        public async Task<UserDto> registerAsync(RegisterDto registerDto)
        {
            //Maping From RegisterDto =>ApplicationUser
            var user = new ApplicationUser()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.UserName,
            };
            //Create User

            var result =await userManager.CreateAsync(user,registerDto.Password);

            if (result.Succeeded)
            {
                return new UserDto()
                {
                    DisplayName = registerDto.DisplayName,
                    Email = registerDto.Email,
                    Token = createTokenAsync(user)
                };
            }
            else 
            {
                var Errors= result.Errors.Select(E=>E.Description).ToList();
                throw new BadRequestException(Errors);
            }
        }

        public static string createTokenAsync(ApplicationUser user)
        {
            return "Token To-Do";
        }
    }
}
