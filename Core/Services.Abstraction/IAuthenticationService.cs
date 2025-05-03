using Shared.Dto;

namespace Services.Abstractions
{
    public interface IAuthenticationService
    {
        //DisPlayName ,email ,Token
       
    
        Task<UserResultDto> Login(LoginDto loginDto);
        Task<UserResultDto> Register(RegisterDto registerDto);
    }
}
