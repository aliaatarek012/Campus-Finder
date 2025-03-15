namespace _CampusFinder.Dto
{
    public class ResetPasswordRequestDto
    {

        public string Token { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; }
    }
}
