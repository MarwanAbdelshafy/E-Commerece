using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shared.Dto_s.BasketDto;
using Shared.Dto_s.IdentityDto;

namespace Presentation.Controllers
{

    public class AuthenticationController(IServicesManager servicesManager) : ApiBaseController
    {
        //login
        [HttpPost("Login")]//Post baseUrl/api/Authentication/Login
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await servicesManager.AuthenticationServices.LoginAsync(loginDto); 
            return Ok(user);
        }


        //Register

        [HttpPost("Register")] //Post baseUrl/api/Authentication/Register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = await servicesManager.AuthenticationServices.registerAsync(registerDto);
            return Ok(user);
        }
    }
}
