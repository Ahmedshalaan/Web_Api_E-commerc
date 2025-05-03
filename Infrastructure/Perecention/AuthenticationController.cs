using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.Dto;

namespace Presentation
{
    public class AuthenticationController(IService_Manager service_Manager) :ApiController
    {
        //Login &resister
        [HttpPost("Login")]// post :BaseUrl/Authentication
        public async Task<ActionResult<UserResultDto>> Login(LoginDto loginDto) 
            => Ok( await service_Manager.authenticationService.Login(loginDto));

        [HttpPost("Register")]
        // post :BaseUrl/Authentication/Register
        public async Task<ActionResult<UserResultDto>> Register(RegisterDto registerDto) 
        =>  Ok(await service_Manager.authenticationService.Register(registerDto));
        

    }
}
