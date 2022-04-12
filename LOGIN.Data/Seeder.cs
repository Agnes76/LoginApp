using LOGIN.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOGIN.Data
{
    public class Seeder
    {
        public async static Task Seed(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, AppDbContext context)
        {
            await context.Database.EnsureCreatedAsync();
            if (!context.Users.Any())
            {
                List<string> roles = new List<string> {"Admin", "Regular"};

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = role });          
                }
            }

            List<AppUser> users = new List<AppUser>
            {
                new AppUser
                {
                    FirstName = "Agnes",
                    LastName = "Ugochukwu",
                    Email = "agnes5@gmail.com",
                    UserName = "agy",
                    PhoneNumber = "09088663311"
                },
                 new AppUser
                 {
                    FirstName = "Esther",
                    LastName = "Ugochukwu",
                    Email = "esther9@gmail.com",
                    UserName = "esty",
                    PhoneNumber = "09088663311"
                 },
                  new AppUser
                  {
                    FirstName = "Chima",
                    LastName = "Chukwuka",
                    Email = "chima@gmail.com",
                    UserName = "chiboy",
                    PhoneNumber = "09088663311"
                  }
            };

            foreach (var user in users)
            {
               await userManager.CreateAsync(user, "Password@123");
                if (user == users[0])
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
                else
                {
                    await userManager.AddToRoleAsync(user, "Regular");
                }
            }
        }
    }
}
