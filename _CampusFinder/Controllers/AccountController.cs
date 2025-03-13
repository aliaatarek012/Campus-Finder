using _CampusFinder.Dto;
using _CampusFinder.Errors;
using _CampusFinderCore.Entities.Identity;
using _CampusFinderCore.Services.Contract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace _CampusFinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseApiController
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAuthService _authService;


        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
           IAuthService authService


            )

        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;

        }

        [HttpPost("register")] //Post: /api/account/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            if (CheckEmailExist(model.Email).Result.Value)
                return BadRequest(new ApiValidationErrorResponse()
                { Errors = new string[] { "this email is already in use!!" } });

            var user = new AppUser()
            {
                DisplayName = model.Displayname,
                Email = model.Email,
                UserName = model.Email.Split("@")[0],

            };
            //Create User and enter Data of user at Database  
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Require Email Confirmation
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                return Ok(new { message = $"Please Confirm Your Email With The Code that you Have Received{code}" });
            }

            return BadRequest(new ApiResponse(400));


        }

        [HttpPost]
        [Route("EmailVerification")]
        public async Task<IActionResult> EmailVerification(string email, string code)
        {
            if (email == null || code == null)
                return BadRequest("Invalid Payload");


            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest("Invalid Payload");

            var isVerified = await _userManager.ConfirmEmailAsync(user, code);
            if (isVerified.Succeeded)
                return Ok(new
                {
                    message = "email confirmed"
                });
            return BadRequest("Something went wrong");

        }

        [AllowAnonymous]
        [HttpPost("login")] //Post: /api/account/login
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            //This Email Not Exist at DB 
            if (user is null)
                return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (result.Succeeded is false)
                return Unauthorized(new ApiResponse(401));

            // If pass and Email True will return object from class UserDto
            return Ok(new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _authService.CreateTokenAsync(user, _userManager)
            });
        }

        [Route("ForgetPassword")]
        [HttpPost]
        public async Task<IActionResult> ForgetPassword([FromBody] RequestForgetPasswordDto request)
        {
            if (ModelState.IsValid)
            {
                // validate user
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                    return BadRequest("Invalid Payload");

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                if (string.IsNullOrEmpty(token))
                    return BadRequest("Something went wrong");

                var callbackUrl = $"https://localhost:7267/resetpassword?email={user.Email}&code={token}";

                // Send Email 
                return Ok(new
                {
                    Token = token,
                    email = user.Email

                });


            }
            return BadRequest("Invalid Payload");

        }

        [Route("ResetPassword")]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Payload");

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return BadRequest("Invalid Payload");

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
            if (result.Succeeded)
            {
                return Ok("Password reset successful.");
            }

            return BadRequest("Something Went Wrong");


        }





        //Get current User that sent request\made Login
        [Authorize]
        [HttpGet] //Get : /api/accounts
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {

            var Email = User.FindFirstValue(ClaimTypes.Email); //Get Email of user that sent request
            var user = await _userManager.FindByEmailAsync(Email); //Get User that sent request by Email
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,

                Email = user.Email,

                Token = await _authService.CreateTokenAsync(user, _userManager)

            });
        }



        [HttpGet("emailexists")] //Get : /api/account/emailexists?email=aliaa.tarek@gamil.com

        public async Task<ActionResult<bool>> CheckEmailExist(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;

        }


        [HttpGet("google-login")]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = "/signin-google" // Redirect to home page after login
            };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }


        [HttpGet("signin-google")]
        public async Task<IActionResult> GoogleCallback()
        {
            // Get external login info from Google
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return Unauthorized("External login information not found.");
            }

            // Try to sign in the user with the external provider
            var result = await _signInManager.ExternalLoginSignInAsync(
                info.LoginProvider,
                info.ProviderKey,
                isPersistent: false);

            if (result.Succeeded)
            {
                // User is already linked to an account; redirect or return token
                return Ok(new
                {
                    Message = "Login successful via Google!",
                    Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                });
            }
            else if (result.IsLockedOut)
            {
                return Unauthorized("Account is locked out.");
            }
            else
            {
                // User does not have an existing account; create a new one
                var user = new AppUser
                {
                    Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                    DisplayName = info.Principal.FindFirstValue(ClaimTypes.Name),
                    UserName = info.Principal.FindFirstValue(ClaimTypes.Email)
                };

                var createResult = await _userManager.CreateAsync(user);
                if (!createResult.Succeeded)
                {
                    return BadRequest(new ApiResponse(400, "Failed to create user."));
                }

                // Link the external login to the new user
                var linkResult = await _userManager.AddLoginAsync(user, info);
                if (!linkResult.Succeeded)
                {
                    return BadRequest(new ApiResponse(400, "Failed to link external login."));
                }

                // Sign in the newly created user
                await _signInManager.SignInAsync(user, isPersistent: false);

                // Return user details or token
                return Ok(new
                {
                    Email = user.Email,
                    DisplayName = user.DisplayName,
                    Token = await _authService.CreateTokenAsync(user, _userManager)
                });
            }

        }

    }
}
