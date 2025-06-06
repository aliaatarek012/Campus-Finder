﻿using _CampusFinderCore.Entities.Identity;
using _CampusFinderCore.Services.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace _CampusFinderService
{
    public class AuthService : IAuthService
    {

        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;

        public AuthService(IConfiguration configuration , UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<(bool IsAuthenticated, string Message, string Token)> CheakResetPassword(string email, string resetCode)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return (false, "Email is incorrect!", null);
            }

            // Validate the reset code and expiry
            if (user.ResetPasswordCode != resetCode || user.ResetCodeExpiry < DateTime.UtcNow)
            {
                return (false, "The reset code is invalid or has expired.", null);
            }

            // Generate a token for resetting the password
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            return (true, "Reset code is valid.", token);
        }
        

        public async Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager)
        {
            //Private Claims (User-Defined)

            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.GivenName,user.UserName),
                new Claim(ClaimTypes.Email,user.Email)
            };

            ///May use Roles as private Claims 
            ///if front want to Know what is role of user(Manager,employee,customer,delivery worker ...) 
            ///To Put roles of users at Token

            var userRoles = await userManager.GetRolesAsync(user);

            foreach (var role in userRoles)
                authClaims.Add(new Claim(ClaimTypes.Role, role));


            //Generate Secret Key to Make encoding to (Header and payload):
            var authkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:AuthKey"])); //put SecretKey at appsettings

            //token: token object use it to build token
            var token = new JwtSecurityToken(
                ///claims : 
                ///Registered claims: These are a set of predefined claims which are not mandatory but recommended,
                ///to provide a set of useful, interoperable claims. Some of them are: iss (issuer),
                ///exp (expiration time), sub (subject), aud (audience)
                ///OR
                ///Private claims: These are the custom claims created to share information between parties

                //1.Put Registered claims
                audience: _configuration["JWT:ValidAudience"],
                issuer: _configuration["JWT:ValidIssuer"],
                expires: DateTime.UtcNow.AddDays(double.Parse(_configuration["JWT:DurationInDays"])),
                //2.Put Private claims
                claims: authClaims,

                //3.Send Secret key and security Algo
                signingCredentials: new SigningCredentials(authkey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature)

                );

            //return Token iteself
            return new JwtSecurityTokenHandler().WriteToken(token);


        }
    }
}