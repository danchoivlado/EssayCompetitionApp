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

    public class AddToRoleSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManeger = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            List<string> contestarsNameList = new List<string>()
            {
                GlobalSeedDataConstants.Contestant1Email,
                GlobalSeedDataConstants.Contestant2Email,
                GlobalSeedDataConstants.Contestant3Email,
                GlobalSeedDataConstants.Contestant4Email,
                GlobalSeedDataConstants.Contestant5Email,
            };
            List<string> teachersNameList = new List<string>()
            {
                GlobalSeedDataConstants.Teacher1Email,
                GlobalSeedDataConstants.Teacher2Email,
                GlobalSeedDataConstants.Teacher3Email,
            };

            await AddAdministratorsToRoleAsync(userManeger, GlobalConstants.AdministratorUserEmail);

            await AddContestantsToRoleAsync(userManeger, GlobalConstants.ContestRoleName, contestarsNameList);

            await AddTeachersToRoleAsync(userManeger, GlobalConstants.TeacherRoleName, teachersNameList);
        }

        private static async Task AddTeachersToRoleAsync(
            UserManager<ApplicationUser> userManeger,
            string teacherRoleName,
            List<string> teachersNameList)
        {
            foreach (var userEmail in teachersNameList)
            {
                var user = await userManeger.FindByNameAsync(userEmail);
                if (user == null)
                {
                    throw new NullReferenceException($"No user with {userEmail} sedded");
                }

                var isInRole = await userManeger.IsInRoleAsync(user, teacherRoleName);
                if (!isInRole)
                {
                    var result = await userManeger.AddToRoleAsync(user, teacherRoleName);

                    if (!result.Succeeded)
                    {
                        throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                    }
                }
            }
        }

        private static async Task AddContestantsToRoleAsync(
            UserManager<ApplicationUser> userManeger,
            string contestantRoleName,
            List<string> contestarsNameList)
        {
            foreach (var userEmail in contestarsNameList)
            {
                var user = await userManeger.FindByNameAsync(userEmail);
                if (user == null)
                {
                    throw new NullReferenceException($"No user with {userEmail} sedded");
                }

                var isInRole = await userManeger.IsInRoleAsync(user, contestantRoleName);
                if (!isInRole)
                {
                    var result = await userManeger.AddToRoleAsync(user, contestantRoleName);

                    if (!result.Succeeded)
                    {
                        throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                    }
                }
            }
        }

        private static async Task AddAdministratorsToRoleAsync(UserManager<ApplicationUser> userManeger, string userEmail)
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
