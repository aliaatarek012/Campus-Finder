using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CampusFinderCore.Entities.Identity
{
    public class AppUser : IdentityUser
    {

        //Customize on properties of Users
        public string DisplayName { get; set; }

        // Add the ResetPasswordCode property
        public string? ResetPasswordCode { get; set; }

        // Add the ResetCodeExpiry property
        public DateTime? ResetCodeExpiry { get; set; }
        public string? VerificationCode { get; set; }


    }
}
