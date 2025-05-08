using Shared.Dto;
using Shared.orderDto;

namespace Services.Abstractions
{
    public interface IAuthenticationService
    {
        //DisPlayName ,email ,Token
       
    
        Task<UserResultDto> Login(LoginDto loginDto);
        Task<UserResultDto> Register(RegisterDto registerDto);
        //Get current user
        Task<UserResultDto> GetUserByEmail(string email);
        //Check if email exists
        Task<bool> CheckEmailExist(string email);
        //Get user by Address
        Task<AddressDto> GetUserByAddress(string email);
        //Update user Address
        Task<AddressDto> UpdateUserAddress(AddressDto addressdto,string email);
    }
}
