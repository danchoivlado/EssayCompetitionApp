namespace EssayCompetition.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using EssayCompetition.Common;
    using EssayCompetition.Data.Models;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    internal class RolesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            await SeedAdministrationRoleAsync(roleManager, GlobalConstants.AdministratorRoleName);

            await SeedTeacherRoleAsync(roleManager, GlobalConstants.TeacherRoleName);

            await SeedContestantRoleAsync(roleManager, GlobalConstants.ContestRoleName);
        }

        private static async Task SeedContestantRoleAsync(RoleManager<ApplicationRole> roleManager, string contestantRoleName)
        {
            var role = await roleManager.FindByNameAsync(contestantRoleName);
            if (role == null)
            {
                var result = await roleManager.CreateAsync(new ApplicationRole(contestantRoleName));
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }

        private static async Task SeedTeacherRoleAsync(RoleManager<ApplicationRole> roleManager, string techerRoleName)
        {
            var role = await roleManager.FindByNameAsync(techerRoleName);
            if (role == null)
            {
                var result = await roleManager.CreateAsync(new ApplicationRole(techerRoleName));
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }

        private static async Task SeedAdministrationRoleAsync(RoleManager<ApplicationRole> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                var result = await roleManager.CreateAsync(new ApplicationRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
