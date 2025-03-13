using _CampusFinderCore.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CampusFinderInfrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {

        public static async Task SeedUsersAsync(UserManager<AppUser> _userManager)
        {
            // if Table Users Not Contain any element
            if (_userManager.Users.Count() == 0)
            {
                var user = new AppUser()
                {
                    DisplayName = "Aliaa Tarek",
                    Email = "aliaa.tarek@gmail.com",
                    UserName = "aliaa.tarek",
                    PhoneNumber = "01234567"
                };

                await _userManager.CreateAsync(user, "Pa$$w0rd");
            }

            //To Update email of user 
            var existingUser = await _userManager.FindByEmailAsync("aliaa.tarek@gmail.com");

            if (existingUser != null)
            {
                existingUser.Email = "yousef.hani@gmail.com";
                await _userManager.UpdateAsync(existingUser);
            }
        }


    }
}
