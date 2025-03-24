using System.ComponentModel.DataAnnotations;

namespace CampusFinder.Dto
{
    public class CheckResetCodeDTO
    {

        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string ResetCode { get; set; }

    }
}
