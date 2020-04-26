namespace EssayCompetition.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EssayCompetition.Common;
    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Data.ContestServices;
    using EssayCompetition.Services.Data.EssayServices;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class EssaySeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var contestService = serviceProvider.GetRequiredService<IContestService>();
            var essayService = serviceProvider.GetRequiredService<IEssayService>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            List<string> contestantsIds = new List<string>();
            List<int> contestIds = new List<int>();
            List<string> teachersIds = new List<string>();

            List<string> teachersName = new List<string>()
            {
                GlobalSeedDataConstants.Teacher1Email,
                GlobalSeedDataConstants.Teacher2Email,
                GlobalSeedDataConstants.Teacher3Email,
            };

            List<string> contestarsName = new List<string>()
            {
                GlobalSeedDataConstants.Contestant1Email,
                GlobalSeedDataConstants.Contestant2Email,
                GlobalSeedDataConstants.Contestant3Email,
                GlobalSeedDataConstants.Contestant4Email,
                GlobalSeedDataConstants.Contestant5Email,
            };

            List<string> contestNames = new List<string>()
            {
                GlobalSeedDataConstants.Contest1,
                GlobalSeedDataConstants.Contest2,
                GlobalSeedDataConstants.Contest3,
                GlobalSeedDataConstants.Contest4,
            };

            foreach (var name in teachersName)
            {
                var user = await userManager.FindByNameAsync(name);
                if (user == null)
                {
                    throw new NullReferenceException($"No user with name {name}");
                }

                teachersIds.Add(user.Id);
            }

            foreach (var name in contestarsName)
            {
                var user = await userManager.FindByNameAsync(name);
                if (user == null)
                {
                    throw new NullReferenceException($"No user with name {name}");
                }

                contestantsIds.Add(user.Id);
            }

            foreach (var name in contestNames)
            {
                if (!contestService.HasContextWithName(name))
                {
                    throw new NullReferenceException($"No contest with name {name}");
                }

                contestIds.Add(contestService.GetContestId(name));
            }

            var essaysInfo = new List<(string Title, string Description, string Content)>
            {
                ("Essay1", "Description1", "Content1"),
                ("Essay2", "Description2", "Content2"),
                ("Essay3", "Description3", "Content3"),
                ("Essay4", "Description4", "Content4"),
                ("Essay5", "Description5", "Content5"),
            };

            await SeedEssays(essayService, teachersIds, contestService, contestIds, contestantsIds, essaysInfo);
        }

        private async Task SeedEssays(IEssayService essayService, List<string> teachersIds, IContestService contestService, List<int> contestIds, List<string> contestarsIds, List<(string Title, string Description, string Content)> essaysInfo)
        {
            foreach (var contestId in contestIds)
            {
                int counter = 0;
                foreach (var contestantId in contestarsIds)
                {
                    if (!essayService.HasUserEssay(contestantId, contestId))
                    {
                        var essay = essaysInfo[counter];
                        await contestService.SeedContestEssayAsync(
                            new Essay
                            {
                                Title = essay.Title,
                                Description = essay.Description,
                                Content = essay.Content,
                                ContestId = contestId,
                                UserId = contestantId,
                            }, teachersIds);
                    }

                    counter++;
                }
            }
        }
    }
}
