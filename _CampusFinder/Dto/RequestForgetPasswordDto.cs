using System.ComponentModel.DataAnnotations;

namespace _CampusFinder.Dto
{
    public class RequestForgetPasswordDto
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;



    }
}
