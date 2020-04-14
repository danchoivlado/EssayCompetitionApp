namespace EssayCompetition.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using EssayCompetition.Common;
    using EssayCompetition.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class UserSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManeger = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await SeedUserAsync(userManeger, GlobalConstants.AdministratorUserEmail, GlobalConstants.AdministratorUserPassword);
        }

        private static async Task SeedUserAsync(UserManager<ApplicationUser> userManeger, string userEmail, string userPassword)
        {
            var user = await userManeger.FindByNameAsync(userEmail);
            if (user == null)
            {
                var result = await userManeger.CreateAsync(
                    new ApplicationUser()
                {
                    Email = userEmail,
                    UserName = userEmail,
                }, userPassword);

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
