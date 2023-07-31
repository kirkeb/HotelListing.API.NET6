using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Models.Users
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Password must be at least 6 and no more than 50 characters",
            MinimumLength = 6)]
        public string Password { get; set; }
    }
}
