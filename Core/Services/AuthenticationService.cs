using Domain.Entities.Idenetity;
using Domain.Entities.orderEntities;
using Domain.Exceptions;
using Domain.Exceptions.NotFoundExcipitions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.Dto;
using Shared.orderDto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Services
{
    public class AuthenticationService(UserManager<User> _userManager, IOptions<JwtOptions> _options,IMapper _mapper) : IAuthenticationService
    {
        public async Task<UserResultDto> Register(RegisterDto registerDto)
        {
            var user = new User
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                PhoneNumber = registerDto.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password); //Becouse I Create Hashing password Password

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
            }
            return new UserResultDto(user.DisplayName, user.Email, await CreateTokenAsync(user));

        }
        public async Task<UserResultDto> Login(LoginDto loginDto)
        {
            // Email is Aready Added in Account 
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) throw new UnAusthorizedException($"Email {loginDto.Email} doesn't Exist: ");

            // Password is Aready Correct or not
            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result) throw new UnAusthorizedException($"Email {loginDto.Password} doesn't Exist: ");
            return new UserResultDto(user.DisplayName, user.Email, await CreateTokenAsync(user));
        } 
        private async Task<string> CreateTokenAsync(User user)
        {
            var jwtoptions = _options.Value;
            //Create Private Claim==>Email
            var PrivateClaims = new List<Claim>
            {
                new(ClaimTypes.Name, user.DisplayName),
                new(ClaimTypes.Email, user.Email)
            };
            //Add Role To Claims if Exsist
            var Roles = await _userManager.GetRolesAsync(user);
            //many to Many
            foreach (var role in Roles)
                PrivateClaims.Add(new Claim(ClaimTypes.Role, role));
            // Create Key 
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtoptions.Secretkey));
            // Create Algorithm
            var Algorithm = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            //Create Token
            var Token = new JwtSecurityToken(
                issuer: jwtoptions.Issuer,              //backend
                audience: jwtoptions?.Audience,
                claims: PrivateClaims,
                 expires: DateTime.UtcNow.AddDays(jwtoptions.DurationInDays)
                 , signingCredentials: Algorithm
                );
            //object Member Method Create object From =====> JwtSecurityTokenHandler
            return new JwtSecurityTokenHandler().WriteToken(Token);

        } 
        public async Task<bool> CheckEmailExist(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null;  //Value is not null ==> true  Value is null ==> false
        } 
        public async Task<AddressDto> GetUserByAddress(string email)
        {
            var user =await _userManager.Users.Include(u=>u.Address)
                                        .FirstOrDefaultAsync(x => x.Email == email)?? throw new User_NotFound_Excption(email);
            return _mapper.Map<AddressDto>(user.Address);

        }
        public async Task<UserResultDto> GetUserByEmail(string email)
        {
            var user =await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email) ?? throw new User_NotFound_Excption(email);
            return new UserResultDto(user.DisplayName, user.Email, await CreateTokenAsync(user));
        } 
        public async Task<AddressDto> UpdateUserAddress(AddressDto addressdto, string email)
        {
            var user = await _userManager.Users.Include(u => u.Address)
                                        .FirstOrDefaultAsync(x => x.Email == email) ?? throw new User_NotFound_Excption(email);
 
        if(user.Address != null)
            {
                user.Address.FirstName = addressdto.FristName;
                user.Address.LastName = addressdto.LastName;
                user.Address.Street = addressdto.Street;
                user.Address.Country = addressdto.Country;
                user.Address.City = addressdto.City;

            }
            else
            {
              var  userAddress = _mapper.Map<Address>(addressdto);
                user.Address = userAddress; 
            }
            await _userManager.UpdateAsync(user);
        return _mapper.Map<AddressDto>(user.Address);
        }
    }
}
