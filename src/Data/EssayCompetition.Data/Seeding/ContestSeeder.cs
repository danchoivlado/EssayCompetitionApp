namespace EssayCompetition.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EssayCompetition.Services.Data.CategoryServices;
    using EssayCompetition.Services.Data.ContestServices;
    using Microsoft.Extensions.DependencyInjection;

    public class ContestSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var categoryService = serviceProvider.GetRequiredService<ICategoryService>();
            var contestService = serviceProvider.GetRequiredService<IContestService>();

            var contestList = new List<(string Name, string Description, DateTime StartTime, DateTime EndTime)>()
            {
                ("Legion1", "Legion1 is for more contestants", DateTime.UtcNow.AddDays(-30), DateTime.UtcNow.AddDays(-30).AddHours(6)),
                ("Legion2", "Legion2 is for more contestants", DateTime.UtcNow.AddDays(-14), DateTime.UtcNow.AddDays(-14).AddHours(6)),
                ("Legion3", "Legion3 is for more contestants", DateTime.UtcNow.AddDays(-7), DateTime.UtcNow.AddDays(-7).AddHours(6)),
                ("Legion4", "Legion4 is for more contestants", DateTime.UtcNow.AddDays(-3), DateTime.UtcNow.AddDays(-3).AddHours(6)),
                ("Legion5", "Legion5 is for more contestants", DateTime.UtcNow.AddDays(2), DateTime.UtcNow.AddDays(2).AddHours(6)),
            };

            await this.SeedContests(categoryService, contestService, contestList);
        }

        private async Task SeedContests(ICategoryService categoryService, IContestService contestService, List<(string Name, string Description, DateTime StartTime, DateTime EndTime)> contestList)
        {
            foreach (var contestInfo in contestList)
            {
                if (!contestService.HasContextWithName(contestInfo.Name))
                {
                    int categoryId = categoryService.GetFirstOrDefaultCategoryId();
                    if (categoryId == default(int))
                    {
                        throw new NullReferenceException($"There are no categories");
                    }

                    await contestService.AddContestAsync(
                        contestInfo.StartTime,
                        contestInfo.EndTime,
                        contestInfo.Name,
                        contestInfo.Description,
                        categoryId);
                }
            }
        }
    }
}
