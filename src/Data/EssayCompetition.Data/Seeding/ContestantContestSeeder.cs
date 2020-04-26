namespace EssayCompetition.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EssayCompetition.Common;
    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Data.ContestServices;
    using EssayCompetition.Services.Data.SignServices;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class ContestantContestSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var signService = serviceProvider.GetRequiredService<ISignService>();
            var contestService = serviceProvider.GetRequiredService<IContestService>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            List<string> contestarsIds = new List<string>();
            List<int> contestIds = new List<int>();
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
                GlobalSeedDataConstants.Contest5,
            };

            foreach (var name in contestarsName)
            {
                var user = await userManager.FindByNameAsync(name);
                if (user == null)
                {
                    throw new NullReferenceException($"No user with name {name}");
                }

                contestarsIds.Add(user.Id);
            }

            foreach (var name in contestNames)
            {
                if (!contestService.HasContextWithName(name))
                {
                    throw new NullReferenceException($"No contest with name {name}");
                }

                contestIds.Add(contestService.GetContestId(name));
            }

            await SingUsersForContest(signService, contestarsIds, contestIds);
        }

        private async Task SingUsersForContest(ISignService signService, List<string> contestarsIds, List<int> contestIds)
        {
            foreach (var contestId in contestIds)
            {
                foreach (var contestantId in contestarsIds)
                {
                    if (!signService.UserAlreadyRegisteredForCompetition(contestantId, contestId))
                    {
                        await signService.RegisterForContestAsync(contestantId, contestId);
                    }
                }
            }
        }
    }
}
