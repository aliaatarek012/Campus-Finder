using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace _CampusFinder.Dto
{
    public class ResetPasswordRequestDto
    {

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string ResetCode { get; set; }


        [Required, PasswordPropertyText]
        public string NewPassword { get; set; }


    }
}
