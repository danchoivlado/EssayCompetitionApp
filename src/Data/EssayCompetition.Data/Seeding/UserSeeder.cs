namespace EssayCompetition.Data.Seeding
{
    using System;
    using System.Collections.Generic;
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

            await SeedAdimnistratorsAsync(userManeger, GlobalConstants.AdministratorUserEmail, GlobalConstants.AdministratorUserPassword);
            await SeedContestarsAsync(userManeger);
            await SeedTeachersAsync(userManeger);
        }

        private static async Task SeedTeachersAsync(UserManager<ApplicationUser> userManeger)
        {
            List<string> teachersNameList = new List<string>()
            {
                GlobalSeedDataConstants.Teacher1Email,
                GlobalSeedDataConstants.Teacher2Email,
                GlobalSeedDataConstants.Teacher3Email,
            };

            foreach (var userEmail in teachersNameList)
            {
                var contestar = await userManeger.FindByNameAsync(userEmail);
                if (contestar == null)
                {
                    var result = await userManeger.CreateAsync(
                        new ApplicationUser()
                        {
                            Email = userEmail,
                            UserName = userEmail,
                        }, GlobalSeedDataConstants.ContestantPassword);

                    if (!result.Succeeded)
                    {
                        throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                    }
                }
            }
        }

        private static async Task SeedContestarsAsync(UserManager<ApplicationUser> userManeger)
        {
            List<string> contestarsNameList = new List<string>()
            {
                GlobalSeedDataConstants.Contestant1Email,
                GlobalSeedDataConstants.Contestant2Email,
                GlobalSeedDataConstants.Contestant3Email,
                GlobalSeedDataConstants.Contestant4Email,
                GlobalSeedDataConstants.Contestant5Email,
            };

            foreach (var userEmail in contestarsNameList)
            {
                var contestar = await userManeger.FindByNameAsync(userEmail);
                if (contestar == null)
                {
                    var result = await userManeger.CreateAsync(
                        new ApplicationUser()
                        {
                            Email = userEmail,
                            UserName = userEmail,
                        }, GlobalSeedDataConstants.ContestantPassword);

                    if (!result.Succeeded)
                    {
                        throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                    }
                }
            }
        }

        private static async Task SeedAdimnistratorsAsync(UserManager<ApplicationUser> userManeger, string userEmail, string userPassword)
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
