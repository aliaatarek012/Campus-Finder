using _CampusFinderCore.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _CampusFinderCore.Services.Contract
{
    public interface IAuthService
    {

        Task<String> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager);


    }
}
