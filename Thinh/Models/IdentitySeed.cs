using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thinh.Models
{
    public class IdentitySeed
    {
        private const string user1 = "Admin1";
        private const string user2 = "Admin2";
        private const string userPassword = "123456";



        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            UserManager<ApplicationUser> userManager = app.ApplicationServices
            .GetRequiredService<UserManager<ApplicationUser>>();
            ApplicationUser _user1 = await userManager.FindByIdAsync(user1);
            ApplicationUser _user2 = await userManager.FindByIdAsync(user2);
            if (_user1 == null)
            {
                _user1 = new ApplicationUser { UserName = user1 ,IsAdmin =1};
                await userManager.CreateAsync(_user1, userPassword);
            }

            if (_user2 == null)
            {
                _user2 = new ApplicationUser { UserName = user2,IsAdmin=1 };
                await userManager.CreateAsync(_user2, userPassword);
            }
        }
    }
}
