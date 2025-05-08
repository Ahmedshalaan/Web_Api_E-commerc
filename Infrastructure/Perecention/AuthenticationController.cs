using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.Dto;
using Shared.orderDto;
using System.Security.Claims;

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

        // Static Segment
        [HttpGet("EmailExists")]  // GET: api/Authentication/EmailExists?email=test@example.com
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
            => Ok(await service_Manager.authenticationService.CheckEmailExist(email));

        // Authenticated User Segment
        [Authorize]
        [HttpGet]  // GET: api/Authentication (Get current user)
        public async Task<ActionResult<UserResultDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            return Ok(await service_Manager.authenticationService.GetUserByEmail(email));
        }

        [Authorize]
        [HttpGet("Address")]  // GET: api/Authentication/Address (Get user address)
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            return Ok(await service_Manager.authenticationService.GetUserByAddress(email));
        }

        [Authorize]
        [HttpPut("Address")]  // PUT: api/Authentication/Address (Update user address)
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto addressDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            return Ok(await service_Manager.authenticationService.UpdateUserAddress(addressDto, email));
        }
        }
}
