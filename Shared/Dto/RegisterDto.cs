using System.ComponentModel.DataAnnotations;

namespace Shared.Dto
{
    public record RegisterDto
    {
        [Required(ErrorMessage = "DisPlay Name is Requird")]
        public string DisplayName { get; init; }
        [Required(ErrorMessage ="Email is Requird")]
        public string Email { get; init; }
        [Required(ErrorMessage = "Password is Requird")]
        public string Password { get; init; }
        [Required(ErrorMessage = "User Name is Requird")]
        public string UserName { get; init; }
         public string? PhoneNumber { get; init; }
    }
}
