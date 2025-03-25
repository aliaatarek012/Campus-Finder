using _CampusFinder.Dto;
using _CampusFinder.Errors;
using _CampusFinderCore.Entities.Identity;
using _CampusFinderCore.Services.Contract;
using CampusFinder.Dto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Security.Claims;
using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace _CampusFinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseApiController
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAuthService _authService;
        private readonly IMailingService _mailingService;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _webHostEnvironment;
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
           IAuthService authService, IMailingService mailingService,
           Microsoft.AspNetCore.Hosting.IHostingEnvironment webHostEnvironment,
            IUrlHelperFactory urlHelperFactory, IHttpContextAccessor httpContextAccessor
                                                )

        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
            _mailingService = mailingService;
            _webHostEnvironment = webHostEnvironment;
            _urlHelperFactory = urlHelperFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("register")] //Post: /api/account/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            if (CheckEmailExist(model.Email).Result.Value)
                return BadRequest(new ApiResponse(400, "This email is already in use!"));

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

                //Verification Code
                var userId = await _userManager.GetUserIdAsync(user);

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var urlHelper = _urlHelperFactory.GetUrlHelper(new ActionContext(
                    _httpContextAccessor.HttpContext,
                    _httpContextAccessor.HttpContext.GetRouteData(),
                    new ActionDescriptor()));


                var verificationUrl = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host
                    + urlHelper.Action("ConfirmEmail", "Account", new { userId = userId, code = code });


                var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot", "EmailTemplate.html");

                if (!System.IO.File.Exists(filePath))
                {
                    throw new FileNotFoundException("Email template not found.", filePath);
                }

                var str = new StreamReader(filePath);

                var mailText = str.ReadToEnd();
                str.Close();

                mailText = mailText.Replace("[name]", user.DisplayName).Replace("[email]", user.Email).Replace("[link]", verificationUrl);
                await _mailingService.SendEmailAsync(user.Email, "verification Code", mailText);


                return Ok(new ApiResponse(200, "Successful registration. Please check your email to confirm."));
            }

            return BadRequest(new ApiResponse(400));


        }



        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                var errorResponse = new ApiResponse(400, "Invalid Payload");
                return BadRequest(errorResponse.ToDictionary());
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                var errorResponse = new ApiResponse(400, $"Unable to load user with ID '{userId}'.");
                return BadRequest(errorResponse.ToDictionary());
            }

            if (await _userManager.IsEmailConfirmedAsync(user))
                return BadRequest(new ApiResponse(400,"Email is already confirmed"));

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (!result.Succeeded)
            {
                return BadRequest(new ApiResponse(400, "Error confirming email."));
            }
            return Ok(new Dictionary<string, string>
                {
                  { "status", "success" },
                  { "message", "email confirmed" }
                });
        }

        [AllowAnonymous]
        [HttpPost("login")] // Post: /api/account/login
        public async Task<ActionResult> Login(LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse(400, "Invalid input."));
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Unauthorized(new ApiResponse(401, "User is not registered. Please sign up."));
            }

            if (!user.EmailConfirmed)
            {
                return BadRequest(new ApiResponse(400, "Email not confirmed."));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized(new ApiResponse(401, "Invalid credentials."));
            }

            return Ok(new Dictionary<string, string>
        {
            { "status", "success" },
            { "displayName", user.DisplayName },
            { "email", user.Email },
            { "token", await _authService.CreateTokenAsync(user, _userManager) }
        });
        }






        [Route("ForgetPassword")]
        [HttpPost]
        public async Task<IActionResult> ForgetPassword([FromBody] RequestForgetPasswordDto request)
        {
            { 
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse(400, "Invalid payload."));
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return BadRequest(new ApiResponse(400, "لم يتم العثور علي الإيميل"));
            }

            // Generate the reset code
            string resetCode = _mailingService.GenerateCode();

                // Set the reset code and expiration time
                user.ResetPasswordCode = resetCode;
                user.ResetCodeExpiry = DateTime.UtcNow.AddMinutes(30); // Expiry time of 30 minutes

                // Save the reset code and expiration time in the database
                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                    return StatusCode(500, new ApiResponse(500, "An error occurred while updating the user record."));

                // Load the email template
                var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot", "ResetPassword.html");
                string emailBody = await System.IO.File.ReadAllTextAsync(filePath);

                // Customize email body with reset code if needed
                emailBody = emailBody.Replace("{ResetCode}", resetCode);

                // Send the reset code via email
                await _mailingService.SendEmailAsync(
                    user.Email,
                    "Code For Reset Password",
                    emailBody // Using the modified template with the reset code
                );

             
            }
            return Ok(new ApiResponse(200, "تم إرسال رمز التأكيد الي ايميلك"));
            //    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            //    if (string.IsNullOrEmpty(token))
            //        return BadRequest("Something went wrong");

            //    var callbackUrl = $"https://localhost:7267/resetpassword?email={user.Email}&code={token}";

            //    // Send Email 
            //    return Ok(new
            //    {
            //        Token = token,
            //        email = user.Email

            //    });


            //}
            //return BadRequest("Invalid Payload");

        }



        //[Route("ResetPassword")]
        //[HttpPost]
        //public async Task<IActionResult> ResetPassword(ResetPasswordRequestDto request)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest("Invalid Payload");

        //    var user = await _userManager.FindByEmailAsync(request.Email);
        //    if (user == null)
        //        return BadRequest("Invalid Payload");

        //    var result = await _userManager.ResetPasswordAsync(user, request.ResetCode, request.NewPassword);
        //    if (result.Succeeded)
        //    {
        //        return Ok("Password reset successful.");
        //    }

        //    return BadRequest("Something Went Wrong");


        //}





        //Get current User that sent request\made Login



        [HttpPost("check-reset-code")]
        public async Task<IActionResult> CheckResetCode([FromBody] CheckResetCodeDTO model)
        {
            // Validate input
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse(400, "Invalid payload."));
            }

            // Convert DTO to plain data
            var (isAuthenticated, message, token) = await _authService.CheakResetPassword(model.Email, model.ResetCode);

            // If authentication fails, return a 400 response with the error message
            if (!isAuthenticated)
            {
                return BadRequest(new Dictionary<string, string>
        {
            { "status", "error" },
            { "message", message }
        });
            }

            return Ok(new Dictionary<string, string>
    {
        { "status", "success" },
        { "message", "Reset code is valid." },
        { "token", token }
    });

        }
        [Authorize]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ChangePassword(ResetPasswordRequestDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse(400, "Invalid payload."));
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest(new ApiResponse(400, "لم يتم العثور علي الإيميل"));


            // Reset the password
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, model.NewPassword);

            if (!result.Succeeded)
            {
                return BadRequest(new ApiResponse(400, "Something went wrong."));
            }

            // Clear the reset code after successful password reset
            user.ResetPasswordCode = null;
            user.ResetCodeExpiry = null;
            await _userManager.UpdateAsync(user);

            return Ok(new ApiResponse(200, "تم تغير كلمة السر بنجاح."));
        }



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
