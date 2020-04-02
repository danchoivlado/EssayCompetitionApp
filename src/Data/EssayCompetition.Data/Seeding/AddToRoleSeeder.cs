namespace EssayCompetition.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using EssayCompetition.Common;
    using EssayCompetition.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class AddToRoleSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManeger = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await AddToRoleAsync(userManeger, GlobalConstants.AdministratorUserEmail);
        }

        private static async Task AddToRoleAsync(UserManager<ApplicationUser> userManeger, string userEmail)
        {
            var user = await userManeger.FindByNameAsync(userEmail);
            if (user == null)
            {
                throw new NullReferenceException($"No user with {GlobalConstants.AdministratorUserEmail} sedded");
            }

            var isInRole = await userManeger.IsInRoleAsync(user, GlobalConstants.AdministratorRoleName);
            if (!isInRole)
            {
                var result = await userManeger.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
