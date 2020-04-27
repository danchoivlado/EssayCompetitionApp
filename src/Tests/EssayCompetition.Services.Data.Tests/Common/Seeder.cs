namespace EssayCompetition.Services.Data.Tests.Common
{
    using System;
    using System.Threading.Tasks;

    using EssayCompetition.Data;
    using EssayCompetition.Data.Models;

    public class Seeder
    {
        public async Task SeedContestAsync(ApplicationDbContext context)
        {
            var category = new Category()
            {
                Title = "Category1",
                Description = "Category1",
                ImageUrl = "#",
            };
            context.Categories.Add(category);
            await context.SaveChangesAsync();

            var contest = new Contest()
            {
                StartTime = DateTime.Now.ToUniversalTime(),
                EndTime = DateTime.Now.ToUniversalTime().AddDays(1),
                CategoryId = category.Id,
                Name = "Legion1",
            };

            context.Contests.Add(contest);
            await context.SaveChangesAsync();
        }

        public async Task<Contest> SeedFutureContestAsync(ApplicationDbContext context)
        {
            var category = new Category()
            {
                Title = "Category1",
                Description = "Category1",
                ImageUrl = "#",
            };
            context.Categories.Add(category);
            await context.SaveChangesAsync();

            var contest = new Contest()
            {
                StartTime = DateTime.Now.ToUniversalTime().AddDays(1),
                EndTime = DateTime.Now.ToUniversalTime().AddDays(2),
                CategoryId = category.Id,
                Name = "Legion2",
            };

            context.Contests.Add(contest);
            await context.SaveChangesAsync();
            return contest;
        }

        public async Task<ApplicationUser> SeedUserAsync(ApplicationDbContext context, string userEmail)
        {
            var user = new ApplicationUser() { Email = userEmail };

            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<ContestantContest> SeedContestantContestAsync(ApplicationDbContext context)
        {
            var user = await this.SeedUserAsync(context, "Test@abv.bg");
            var contest = await this.SeedFutureContestAsync(context);

            var contestantContest = new ContestantContest()
            {
                ContestantId = user.Id,
                ContestId = contest.Id,
            };

            context.ContestantContest.Add(contestantContest);
            await context.SaveChangesAsync();
            return contestantContest;
        }
    }
}
